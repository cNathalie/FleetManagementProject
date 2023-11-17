using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FleetController : ControllerBase
    {
        private readonly IFMFleetRepository _repository;
        private readonly IMapper _mapper;

        public FleetController(IFMFleetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpGet]
        public ActionResult<IEnumerable<FleetMemberDTO>> Get()
        {
            return Ok(_mapper.Map<List<FleetMemberDTO>>(_repository.Fleet.ToList()));
        }
    }
}
