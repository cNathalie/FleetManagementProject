using AutoMapper;
using EF_Repositories;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FM_API;

[ApiController]
[Route ("[controller]")]
public class TankkaartenController : ControllerBase
{
    private readonly IFMTankkaartRepository _repository;
    private readonly IMapper _mapper;

    public TankkaartenController(IFMTankkaartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet(Name = "GetTankkaarten")]
    public ActionResult<IEnumerable<TankkaartDTO>> Get()
    {
        return Ok(_mapper.Map<List<TankkaartDTO>>(_repository.Tankkaarten.ToList()));
    }
}
