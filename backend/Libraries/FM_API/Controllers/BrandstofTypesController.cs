using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandstofTypesController : ControllerBase
    {
        private readonly IFMBrandstoftypeRepository _repository;
        private readonly IMapper _mapper;

        public BrandstofTypesController(IFMBrandstoftypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpGet(Name = "GetBrandstoffen")]
        public ActionResult<IEnumerable<BrandstofTypeDTO>> Get()
        {
            return Ok(_mapper.Map<List<BrandstofTypeDTO>>(_repository.Brandstoffen.ToList()));
        }

    }
}
