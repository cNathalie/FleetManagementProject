using FM_Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FM_API
{
    public class JWTokenService
    {
        private readonly IConfiguration _configuration;
        // Moet naar configuratie verplaats worden voor veiligheid
        private readonly string _issuer = "http://localhost:5100/";
        private readonly string _audience = "http://localhost:5100/";
        private readonly string _secretKey = "RanmKUsWWkCdQohoQy2UitQx2MKr5R0m";

        public JWTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string email, FMRole role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: credentials
            );
            Debug.WriteLine(token);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //public ClaimsPrincipal ValidateToken(string token)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var validationParameters = GetValidationParameters();

        //    try
        //    {
        //        // Validate the token
        //        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

        //        return principal;
        //    }
        //    catch (SecurityTokenValidationException ex)
        //    {
        //        Debug.WriteLine($"SecurityTokenValidationException: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Token validation failed: {ex.Message}");
        //    }
        //    return null;
        //}

        //private TokenValidationParameters GetValidationParameters()
        //{
        //    return new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true, 
        //        ValidAudience = _audience,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ValidIssuer = _issuer,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
        //    };
        //}
    }
}

