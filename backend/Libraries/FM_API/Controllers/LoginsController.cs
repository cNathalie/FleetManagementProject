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

        // Authenticatie verplaatst naar eigen controller

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

        [HttpDelete("id", Name = "DeleteLoginById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([Required] int id)
        {
            var login = _repository.Logins.Where(l => l.LoginId == id).FirstOrDefault();
            if (login == null)
            {
                return BadRequest("Id not found");
            }

            _repository.Delete(login);
            return NoContent();
        }

        [HttpDelete("email", Name = "DeleteLoginByEmail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([Required][FromBody] string email)
        {
            var login = _repository.Logins.Where(l => l.Email == email.Trim()).FirstOrDefault();
            if (login == null)
            {
                return BadRequest("Email not found");
            }

            _repository.Delete(login);
            return NoContent();
        }

    }
}
