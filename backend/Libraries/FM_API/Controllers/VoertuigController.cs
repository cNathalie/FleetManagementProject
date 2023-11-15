using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoertuigController : ControllerBase
    {
        private readonly EFVoertuigRepository _repository;
        private readonly IMapper _mapper;

        public VoertuigController(EFVoertuigRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VoertuigDTO>> Get()
        {
            return Ok(_mapper.Map<List<VoertuigDTO>>(_repository.Voertuigen.ToList()));
        }
    }
}
