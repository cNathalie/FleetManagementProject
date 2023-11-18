using AutoMapper;
using FM_API.DTO;
using FM_Domain;
using FM_Domain.Enums;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class LoginsController : ControllerBase
    {
        private readonly IFMLoginRepository _repository;
        private readonly IMapper _mapper;

        public LoginsController(IFMLoginRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetLogins")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LoginDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<LoginDTO>> Get()
        {
            return Ok(_mapper.Map<List<LoginDTO>>(_repository.Logins.ToList()));
        }

        // In React een dto met emailadres en wachtwoord versturen met deze request
        // Geeft de rol van de gebruiker terug
        [HttpPost("auth", Name = "AuthenticateLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<String> GetAuthentication(AuthLoginDTO login)
        {
            var response = _repository.Authenticate(_mapper.Map<Login>(login));
            if(response == FMRole.None)
            {
                return BadRequest("Wrong username or password");
            }
            return Ok(response.ToString());
        }

        // In React een dto versturen waarin emailadres, wachtwoord en rol zijn ingevuld
        [HttpPost (Name = "CreateNewUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([Required][FromBody] LoginDTO nieuweLogin)
        {
            if (nieuweLogin == null)
            {
                return BadRequest("Invalid request data");
            }

            var aangemaaktLogin = _repository.Insert(_mapper.Map<Login>(nieuweLogin));
            return CreatedAtAction(nameof(Get), new { id = aangemaaktLogin.LoginId }, _mapper.Map<LoginDTO>(aangemaaktLogin));
        }

    }
}
