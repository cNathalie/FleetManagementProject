using FM.Domain.Models;
using FM.Infrastructure.EntityFramework.Context;
using FM.Infrastructure.EntityFramework.Models;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Mappers;
using FM.Infrastructure.Resources;
using FM.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace FM.Infrastructure.Services;

public sealed class EncryptedUserService : IEncryptedUserService
{
    private readonly IConfiguration _configuration;
    private readonly FleetManagementDbContext _context;
    private readonly IJWTManager _jwtManager;
    private readonly string _pepper;
    private readonly int _iteration = 3;

    public EncryptedUserService(FleetManagementDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration, IJWTManager jwtService)
    {
        _context = context;
        _configuration = configuration;
        _pepper = _configuration["Pepper"]!;
        _jwtManager = jwtService;
    }


    public async Task<UserResource> Register(RegisterResource resource, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = resource.Email,
            PasswordSalt = PasswordHasher.GenerateSalt(),
            Role = resource.Role,
        };
        user.PasswordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt, _pepper, _iteration);
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new UserResource(user.UserId, user.Email, user.Role);
    }

    public async Task<AuthenticatedUserResource> Login(LoginResource resource, CancellationToken cancellationToken)
    {
        // Check if user exists
        if(IsValidUserAsync(resource, cancellationToken).Result == false)
            throw new EntityDoesNotExistException("User does not exist.");
   
        // Get user
        var user = await GetUserAsync(resource, cancellationToken);
        var passwordHash = PasswordHasher.ComputeHash(resource.Password, user.PasswordSalt!, _pepper, _iteration);
        if (user.PasswordHash != passwordHash)
            throw new UnauthorizedAccessException("Username or password did not match.");

        // Get jwt token
        var tokens = _jwtManager.GenerateToken(new UserResource(user.UserId, user.Email!, user.Role.ToString()!));
        await AddUserRefreshTokens(new DUserRefreshToken(user.UserId, tokens.RefreshToken, true));
        return new AuthenticatedUserResource(user.UserId, user.Email!, user.Role.ToString()!, tokens);
    }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<string> Verify(Tokens tokens)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var principal = _jwtManager.GetPrincipalFromExpiredToken(tokens.AccessToken!);
        var userRole = principal.FindFirst("Role")!.Value;
        return userRole;
    }

    public async Task<Tokens> RefreshToken(Tokens tokens)
    {
        var principal = _jwtManager.GetPrincipalFromExpiredToken(tokens.AccessToken!);
        var userId = Int32.Parse(principal.FindFirst("UserId")!.Value);

        var savedRefreshToken = await GetSavedRefreshTokens(userId!, tokens.RefreshToken!);
        if (savedRefreshToken.RefreshToken != tokens.RefreshToken)
        {
            throw new UnauthorizedAccessException("Invalid RefreshToken");
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId! && x.IsDeleted == false);
        var newJwtTokens = _jwtManager.GenerateRefreshToken(new UserResource(user!.UserId, user.Email, user.Role));

        if (newJwtTokens == null)
        {
            throw new UnauthorizedAccessException("Invalid attempt");
        }

        await DeleteUserRefreshTokens(userId!, tokens.RefreshToken!);
        await AddUserRefreshTokens(new DUserRefreshToken(userId!, newJwtTokens.RefreshToken, true));

        return newJwtTokens;
    }



    public async Task<bool> IsValidUserAsync(LoginResource resource, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync(resource, cancellationToken);
        if (user != null)
            return true;
        else
            return false;
    }

    public async Task<DUser> GetUserAsync(LoginResource resource, CancellationToken cancellationToken)
    {
        var user = await _context.Users
           .FirstOrDefaultAsync(x => x.Email == resource.Email && x.IsDeleted == false, cancellationToken)
           ?? throw new EntityDoesNotExistException("User does not exist");

        return UserMapper.MapToDUser(user!);
    }


    public async Task<DUserRefreshToken> AddUserRefreshTokens(DUserRefreshToken user)
    {
        _context.UserRefreshTokens.Add(UserRefreshTokenMapper.MapToEf(user));
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<DUserRefreshToken> GetSavedRefreshTokens(int userId, string refreshtoken)
    {

        var refreshToken = await _context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == userId && x.RefreshToken == refreshtoken && x.IsActive == true)
             ?? throw new Exception();
        return UserRefreshTokenMapper.MapToDomain(refreshToken);

    }


#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task DeleteUserRefreshTokens(int userId, string refreshToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    {
        var item = _context.UserRefreshTokens.FirstOrDefault(x => x.UserId == userId && x.RefreshToken == refreshToken);
        if (item != null)
        {
            _context.UserRefreshTokens.Remove(item);
            _context.SaveChanges(); //Calling the sync save changes to bypass soft delete
        }
    }

    public async Task Logout(Tokens tokens)
    {
        var principal = _jwtManager.GetPrincipalFromExpiredToken(tokens.AccessToken!);
        var userId = Int32.Parse(principal.FindFirst("UserId")!.Value);
        await DeleteUserRefreshTokens(userId, tokens.RefreshToken!);
    }
}