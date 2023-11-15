using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeWagenController : ControllerBase
    {
        private readonly EFTypeWagenRepository _repository;
        private readonly IMapper _mapper;
        public TypeWagenController(EFTypeWagenRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeWagenDTO>> Get() 
        {
            return Ok(_mapper.Map<List<TypeWagenDTO>>(_repository.TypeWagens.ToList()));
        }
    }
}
