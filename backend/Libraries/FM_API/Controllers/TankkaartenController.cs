using AutoMapper;
using EF_Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FM_API;

[ApiController]
[Route ("[controller]")]
public class TankkaartenController : ControllerBase
{
    private readonly EFTankkaartRepository _repository;
    private readonly IMapper _mapper;

    public TankkaartenController(EFTankkaartRepository repo, IMapper mapper)
    {
        _repository = repo;
        _mapper = mapper;
    }


    [HttpGet(Name = "GetTankkaarten")]
    public ActionResult<IEnumerable<TankkaartDTO>> Get()
    {
        return Ok(_mapper.Map<List<TankkaartDTO>>(_repository.Tankkaarten.ToList()));
    }
}
