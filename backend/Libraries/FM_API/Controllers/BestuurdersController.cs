using AutoMapper;
using EF_Repositories;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM_API;

[ApiController]
[Route("[controller]")]
public class BestuurdersController : ControllerBase
{
    private readonly IFMBestuurderRepository _repository;
    private readonly IMapper _mapper;

    public BestuurdersController(IFMBestuurderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }



    [HttpGet(Name = "GetBestuurders")]
    public ActionResult<IEnumerable<BestuurderDTO>> Get()
    {
        return Ok(_mapper.Map<List<BestuurderDTO>>(_repository.Bestuurders.ToList()));
    }
}
