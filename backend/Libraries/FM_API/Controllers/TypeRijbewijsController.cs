using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
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
    public class TypeRijbewijsController : ControllerBase
    {
        private readonly EFTypeRijbewijsRepository _repository;
        private readonly IMapper _mapper;

        public TypeRijbewijsController(EFTypeRijbewijsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TypeRijbewijsDTO>> Get() 
        {
            return Ok(_mapper.Map<List<TypeRijbewijsDTO>>(_repository.TypesRijbewijs.ToList()));

        }
    }
}
