using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public ActionResult<IEnumerable<LoginDTO>> Get()
        {
            return Ok(_mapper.Map<List<LoginDTO>>(_repository.Logins.ToList()));
        }


    }
}
