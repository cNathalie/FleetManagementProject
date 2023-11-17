using AutoMapper;
using EF_Repositories;
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


        [HttpPost(Name = "PostVoortuig")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put([Required][FromBody] VoertuigDTO nieuwVoertuigDTO)
        {
            if(nieuwVoertuigDTO == null)
            {
                return BadRequest("Invalid request data");
            }

            var aangemaaktVoertuig = _repository.Insert(_mapper.Map<Voertuig>(nieuwVoertuigDTO));
            return CreatedAtAction(nameof(Get), new { id = aangemaaktVoertuig.VoertuigId}, _mapper.Map<VoertuigDTO>(aangemaaktVoertuig));
        }
    }
}
