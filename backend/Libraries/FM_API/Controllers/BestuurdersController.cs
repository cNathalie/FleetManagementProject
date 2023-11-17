using AutoMapper;
using FM_Domain;
using FM_Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FM_API;

[ApiController]
[Route("[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BestuurderDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<BestuurderDTO>> Get()
    {
        return Ok(_mapper.Map<List<BestuurderDTO>>(_repository.Bestuurders.ToList()));
    }

    [HttpGet("id", Name = "GetBestuurderById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BestuurderDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<BestuurderDTO>> GetById([Required] int id)
    {
        var bestuurder = _repository.Bestuurders.Where(b => b.BestuurderId == id).FirstOrDefault();
        if (bestuurder == null)
        {
            return BadRequest("Id not found");
        }
        return Ok(_mapper.Map<BestuurderDTO>(bestuurder));
    }

    //Om een bestuurder toe te voegen vanuit react
    // -> klikken op toevoegen
    // -> de velden van een bestuurde dto moeten kunnen ingevuld worden BEHALVE ID
    // -> de keuze van type rijbewijs moet een dropdown list zijn van de beschikbare types in de databank (zo kunnen er geen onbestaande types worden meegegeven)
    // -> de dto wordt verstuurd in de body van deze post request
    [HttpPost(Name = "PostBestuurder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Post([Required][FromBody] BestuurderDTO nieuweBestuurderDTO)
    {
        if (nieuweBestuurderDTO == null)
        {
            return BadRequest("Invalid request data");
        }

        var aangemaakteBestuurder = _repository.Insert(_mapper.Map<Bestuurder>(nieuweBestuurderDTO));
        return CreatedAtAction(nameof(Get), new { id = aangemaakteBestuurder.BestuurderId }, _mapper.Map<BestuurderDTO>(aangemaakteBestuurder));
    }

    //Om een bestuurder te bewerken vanuit React:
    // -> de knop om te bewerken wordt bij de betreffende bestuurder aangeklikt
    // -> het bewerkvenster krijgt het dto object van die bestuurder mee
    // -> alles BEHALVE ID in het dto object kan bewerkt worden 
    // -> de keuze van type rijbewijs moet een dropdown list zijn van de beschikbare types in de databank (zo kunnen er geen onbestaande types worden meegegeven)
    // -> bij opslaan wordt deze put request verstuurd met het dto object in de body
    [HttpPut(Name = "UpdateBestuurder")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Put([Required][FromBody] BestuurderDTO bestuurderDTO)
    {
        if (bestuurderDTO == null)
        {
            return BadRequest();
        }

        _repository.Update(_mapper.Map<Bestuurder>(bestuurderDTO));
        return Ok("Updatet");
    }

    //Om een bestuurder te deleten vanuit React:
    // -> de knop om te deleten wordt bij de betreffende bestuurder aangeklikt
    // -> er wordt om bevestiging gevraagd
    // -> deze delete request wordt verstuurd met de id van betreffende bestuurder
    [HttpDelete("id", Name = "DeleteBestuurderById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([Required] int id)
    {
        var bestuurder = _repository.Bestuurders.Where(b => b.BestuurderId == id).FirstOrDefault();
        if (bestuurder == null)
        {
            return BadRequest("Id not found");
        }

        _repository.Delete(bestuurder);
        return NoContent();
    }
}
