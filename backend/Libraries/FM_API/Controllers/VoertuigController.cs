using AutoMapper;
using FM_API.DTO;
using FM_Domain;
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
    public class VoertuigController : ControllerBase
    {
        private readonly IFMVoertuigRepository _repository;
        private readonly IMapper _mapper;

        public VoertuigController(IFMVoertuigRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetVoertuigen")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<VoertuigDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<VoertuigDTO>> Get()
        {
            return Ok(_mapper.Map<List<VoertuigDTO>>(_repository.Voertuigen.ToList()));
        }

        [HttpGet("id", Name = "GetVoertuigById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VoertuigDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<VoertuigDTO>> GetById([Required] int id)
        {
            var bestuurder = _repository.Voertuigen.Where(v => v.VoertuigId == id).FirstOrDefault();
            if (bestuurder == null)
            {
                return BadRequest("Id not found");
            }
            return Ok(_mapper.Map<VoertuigDTO>(bestuurder));
        }


        [HttpPost(Name = "PostVoortuig")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([Required][FromBody] VoertuigDTO nieuwVoertuigDTO)
        {
            if(nieuwVoertuigDTO == null)
            {
                return BadRequest("Invalid request data");
            }

            var aangemaaktVoertuig = _repository.Insert(_mapper.Map<Voertuig>(nieuwVoertuigDTO));
            return CreatedAtAction(nameof(Get), new { id = aangemaaktVoertuig.VoertuigId}, _mapper.Map<VoertuigDTO>(aangemaaktVoertuig));
        }

        [HttpPut(Name = "UpdateVoertuig")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([Required][FromBody] VoertuigDTO bestuurderDTO)
        {
            if (bestuurderDTO == null)
            {
                return BadRequest();
            }

            _repository.Update(_mapper.Map<Voertuig>(bestuurderDTO));
            return Ok("Updatet");
        }

        [HttpDelete("id", Name = "DeleteVoertuigById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete([Required] int id)
        {
            var bestuurder = _repository.Voertuigen.Where(v => v.VoertuigId == id).FirstOrDefault();
            if (bestuurder == null)
            {
                return BadRequest("Id not found");
            }

            _repository.Delete(bestuurder);
            return NoContent();
        }

    }
}
