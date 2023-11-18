using AutoMapper;
using EF_Repositories;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FM_API;

[ApiController]
[Route ("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TankkaartDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<TankkaartDTO>> Get()
    {
        return Ok(_mapper.Map<List<TankkaartDTO>>(_repository.Tankkaarten.ToList()));
    }

    [HttpGet("id", Name = "GetTankkaartById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TankkaartDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<TankkaartDTO>> GetById([Required] int id)
    {
        var tankkaart = _repository.Tankkaarten.Where(t => t.TankkaartId == id).FirstOrDefault();
        if (tankkaart == null)
        {
            return BadRequest("Id not found");
        }
        return Ok(_mapper.Map<TankkaartDTO>(tankkaart));
    }

    [HttpPost(Name = "PostTankkaart")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([Required][FromBody] TankkaartDTO nieuweTankkaart)
    {
        if (nieuweTankkaart == null)
        {
            return BadRequest("Invalid request data");
        }

        var aangemaakteTankkaart = _repository.Insert(_mapper.Map<Tankkaart>(nieuweTankkaart));
        return CreatedAtAction(nameof(Get), new { id = aangemaakteTankkaart.TankkaartId }, _mapper.Map<TankkaartDTO>(aangemaakteTankkaart));
    }

    [HttpPut(Name = "UpdateTankkaart")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Put([Required][FromBody] TankkaartDTO tankkaartDTO)
    {
        if (tankkaartDTO == null)
        {
            return BadRequest();
        }

        _repository.Update(_mapper.Map<Tankkaart>(tankkaartDTO));
        return Ok("Updatet");
    }

    [HttpDelete("id", Name = "DeleteTankkaartById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([Required] int id)
    {
        var bestuurder = _repository.Tankkaarten.Where(t => t.TankkaartId == id).FirstOrDefault();
        if (bestuurder == null)
        {
            return BadRequest("Id not found");
        }

        _repository.Delete(bestuurder);
        return NoContent();
    }
}
