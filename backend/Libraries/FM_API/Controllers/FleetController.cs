using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class FleetController : ControllerBase
    {
        private readonly IFMFleetRepository _repository;
        private readonly IMapper _mapper;

        public FleetController(IFMFleetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpGet(Name = "GetFleetMembers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FleetMemberDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FleetMemberDTO>> Get()
        {
            return Ok(_mapper.Map<List<FleetMemberDTO>>(_repository.Fleet.ToList()));
        }


        [HttpGet("id", Name = "GetFleetMemberById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FleetMemberDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FleetMemberDTO>> GetById([Required] int id)
        {
            var bestuurder = _repository.Fleet.Where(fm => fm.FleetMemberId == id).FirstOrDefault();
            if (bestuurder == null)
            {
                return BadRequest("Id not found");
            }
            return Ok(_mapper.Map<BestuurderDTO>(bestuurder));
        }

        [HttpPost(Name = "PostFleetMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([Required][FromBody] FleetMemberDTO nieuweFleetMemberDTO)
        {
            if (nieuweFleetMemberDTO == null)
            {
                return BadRequest("Invalid request data");
            }

            var aangemaaktFleetMember = _repository.Insert(_mapper.Map<FleetMember>(nieuweFleetMemberDTO));
            return CreatedAtAction(nameof(Get), new { id = aangemaaktFleetMember.FleetMemberId }, _mapper.Map<FleetMemberDTO>(aangemaaktFleetMember));
        }
    }
}
