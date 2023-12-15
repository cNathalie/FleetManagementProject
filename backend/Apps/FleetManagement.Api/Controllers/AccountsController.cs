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

        [AllowAnonymous]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticatedUserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.Login(_mapper.Map<LoginResource>(resource), cancellationToken);
                return Ok(_mapper.Map<AuthenticatedUserDTO>(response));
            }
            catch (Exception e)
            {
                _logger.LogError("UserController: " + e.Message, e);
                if (e is UnauthorizedAccessException || e is EntityDoesNotExistException)
                    return Unauthorized(new { ErrorMessage = e.Message });
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }


        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] TokensDTO resource)
        {
            try
            {
                var response = await _userService.RefreshToken(_mapper.Map<Tokens>(resource));
                return Ok(_mapper.Map<TokensDTO>(response));
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
