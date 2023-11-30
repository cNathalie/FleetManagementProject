using FM_Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using API_DTO;
using System.ComponentModel.DataAnnotations;
using FM_Domain;
using System.Net.Mime;

namespace FM_API_Async.Controllers
{
    [ApiController]
    [Route("bestuurders")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BestuurderControllerAsync : ControllerBase
    {
        private readonly IFMBestuurderRepository _repository;
        private readonly IMapper _mapper;

        public BestuurderControllerAsync(IFMBestuurderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetBestuurders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BestuurderDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BestuurderDTO>>> GetAsync()
        {
            var bestuurders = await _repository.GetBestuurdersAsync();
            return Ok(_mapper.Map<List<BestuurderDTO>>(bestuurders));
        }


        [HttpGet(Name = "GetBestuurderById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BestuurderDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BestuurderDTO>> GetByIdAsync([Required] int id)
        {
            var bestuurder = await _repository.GetBestuurderByIdAsync(id);
            if (bestuurder == null)
            {
                return BadRequest("Id not found");
            }
            return Ok(_mapper.Map<BestuurderDTO>(bestuurder));
        }

        [HttpPost(Name = "PostBestuurder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([Required][FromBody] BestuurderDTO nieuweBestuurderDTO)
        {
            if (nieuweBestuurderDTO == null)
            {
                return BadRequest("Invalid request data");
            }

            var aangemaakteBestuurder = await _repository.InsertAsync(_mapper.Map<Bestuurder>(nieuweBestuurderDTO));
            return CreatedAtAction(nameof(GetAsync), new { id = aangemaakteBestuurder.BestuurderId }, _mapper.Map<BestuurderDTO>(aangemaakteBestuurder));
        }

        [HttpPut(Name = "UpdateBestuurder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([Required][FromBody] BestuurderDTO bestuurderDTO)
        {
            if (bestuurderDTO == null)
            {
                return BadRequest();
            }

            await _repository.UpdateAsync(_mapper.Map<Bestuurder>(bestuurderDTO));
            return Ok("Updated");
        }

        [HttpDelete("{id}", Name = "DeleteBestuurderById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([Required] int id)
        {
            var bestuurder = await _repository.GetBestuurderByIdAsync(id);
            if (bestuurder == null)
            {
                return BadRequest("Id not found");
            }

            await _repository.DeleteAsync(bestuurder);
            return NoContent();
        }
    }
}
