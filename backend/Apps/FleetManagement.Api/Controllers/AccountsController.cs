using AutoMapper;
using FleetManagement.Api.DTOs.User;
using FM.Domain.Interfaces;
using FM.Infrastructure.Exceptions;
using FM.Infrastructure.Resources;
using FM.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FleetManagement.Api.Controllers
{
    [ApiController]
    [Route("accounts")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class AccountsController : ControllerBase
    {
        private readonly IEncryptedUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountsController> _logger;
        private readonly IUserRepository _repository;

        public AccountsController(IEncryptedUserService userService, IMapper mapper, ILogger<AccountsController> logger, IUserRepository repo)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _repository = repo;
        }

        [Authorize]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.Register(_mapper.Map<RegisterResource>(resource), cancellationToken);
                return Ok(_mapper.Map<UserOutgoingDTO>(response));
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserRoleDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.Login(_mapper.Map<LoginResource>(resource), cancellationToken);
                Response.Cookies.Append("accessToken", response.Tokens!.AccessToken!, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(15),
                    HttpOnly = true, // Preventing JavaScript access to the cookie
                    Secure = true // Send the cookie over HTTPS
                });
                Response.Cookies.Append("refreshToken", response.Tokens!.RefreshToken!, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(15),
                    HttpOnly = true, 
                    Secure = true 
                });

                var userDTO = new UserRoleDTO()
                {
                    Role = response.Role
                };

                return Ok(userDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                if (e is UnauthorizedAccessException || e is EntityDoesNotExistException)
                    return Unauthorized(new { ErrorMessage = e.Message });
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [Authorize]
        [HttpPost("verify")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Verify()
        {
            try
            {
                var tokens = new Tokens()
                {
                    AccessToken = Request.Cookies["accessToken"],
                    RefreshToken = Request.Cookies["refreshToken"]
                };

                var userRole = await _userService.Verify(tokens);
                var userDTO = new UserRoleDTO()
                {
                    Role = userRole
                };
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                    return BadRequest();;
            }
        }


        [Authorize]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                var tokens = new Tokens()
                {
                    AccessToken = Request.Cookies["accessToken"],
                    RefreshToken = Request.Cookies["refreshToken"]
                };

                var response = await _userService.RefreshToken(_mapper.Map<Tokens>(tokens));

                Response.Cookies.Append("accessToken", response.AccessToken!, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(5),
                    HttpOnly = true, // Prevent JavaScript access to the cookie
                    Secure = true // Only send the cookie over HTTPS
                });
                Response.Cookies.Append("refreshToken", response.RefreshToken!, new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddMinutes(5),
                    HttpOnly = true, // Prevent JavaScript access to the cookie
                    Secure = true // Only send the cookie over HTTPS
                });
                return Ok("Token refreshed");
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                if (e is UnauthorizedAccessException || e is EntityDoesNotExistException)
                    return Unauthorized(new { ErrorMessage = e.Message });
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var tokens = new Tokens()
                {
                    AccessToken = Request.Cookies["accessToken"],
                    RefreshToken = Request.Cookies["refreshToken"]
                };

                await _userService.Logout(_mapper.Map<Tokens>(tokens));
                Response.Cookies.Delete("accessToken");
                Response.Cookies.Delete("refreshToken");
                return Ok("Logged out");
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var users = await _repository.GetAllAsync();
                var usersDTO = _mapper.Map<List<UserOutgoingDTO>>(users);
                return Ok(usersDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var user = await _repository.GetByIdAsync(id);
                var userDTO = _mapper.Map<UserOutgoingDTO>(user);
                return Ok(userDTO);
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByIdAsync([Required] int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("UserController: " + ex.Message, ex);
                return BadRequest(new { ErrorMessage = ex.Message });
            }
        }
    }
}
