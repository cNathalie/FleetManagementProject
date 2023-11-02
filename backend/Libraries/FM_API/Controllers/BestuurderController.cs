using AutoMapper;
using EF_Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace FM_API;

[ApiController]
[Route("[controller]")]
public class BestuurderController : ControllerBase
{
    private readonly EFBestuurderRepository _repository;
    private readonly IMapper _mapper;

    public BestuurderController(EFBestuurderRepository repo, IMapper mapper)
    {
        _repository = repo;
        _mapper = mapper;
    }


    [HttpGet(Name = "GetBestuurders")]
    public ActionResult<IEnumerable<BestuurderDTO>> Get()
    {
        return Ok(_mapper.Map<List<BestuurderDTO>>(_repository.Bestuurders.ToList()));
    }
}
