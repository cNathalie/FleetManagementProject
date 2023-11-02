using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginsController : ControllerBase
    {
        private readonly EFLoginRepository _repository;
        private readonly IMapper _mapper;

        public LoginsController(EFLoginRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetLogins")]
        public ActionResult<IEnumerable<LoginDTO>> Get()
        {
            return Ok(_mapper.Map<List<LoginDTO>>(_repository.Logins.ToList()));
        }


    }
}
