using AutoMapper;
using FM_API.DTO;
using FM_Domain;
using FM_Domain.Enums;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JWTokenService _tokenService;
        private readonly IFMLoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public AuthController(JWTokenService tokenService, IFMLoginRepository loginRepository, IMapper mapper) 
        {
            _tokenService = tokenService;
            _loginRepository = loginRepository;
            _mapper = mapper;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthLoginDTO login)
        {
            // Validate user credentials

            var roleResponse = _loginRepository.Authenticate(_mapper.Map<Login>(login)) ;

            if(roleResponse == FMRole.None)
            {
                return BadRequest(roleResponse);
            }

            var email = login.Email; 
            var role = roleResponse; 

            // Generate JWT token
            var token = _tokenService.GenerateJwtToken(email, role);

            return Ok(new { Token = token, Role = role.ToString() });
        }


        //[HttpGet("test")]
        //public IActionResult TestValidation(string accesToken)
        //{
        //    var response = _tokenService.ValidateToken(accesToken);
        //    if(response == null)
        //    {
        //        return BadRequest("invalid token");
        //    }
        //    return Ok(response);
        //}

    }
}
