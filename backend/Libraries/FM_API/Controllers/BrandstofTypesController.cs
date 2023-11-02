using AutoMapper;
using EF_Repositories;
using FM_API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FM_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandstofTypesController : ControllerBase
    {
        private readonly EFBrandstofTypeRepository _repository;
        private readonly IMapper _mapper;

        public BrandstofTypesController(EFBrandstofTypeRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetBrandstoffen")]
        public ActionResult<IEnumerable<BrandstofTypeDTO>> Get()
        {
            return Ok(_mapper.Map<List<BrandstofTypeDTO>>(_repository.Brandstoffen.ToList()));
        }

    }
}
