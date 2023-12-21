using AutoMapper;
using FleetManagement.Api.DTOs.Voertuig;
using FleetManagement.Api.Extensions;
using FM.Domain.Exceptions;
using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FleetManagement.Api.Controllers
{
    [ApiController]
    [Route("voertuigen")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class VoertuigenController : ControllerBase
    {
        private readonly IVoertuigRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<VoertuigenController> _logger;

        public VoertuigenController(IVoertuigRepository repository, IMapper mapper, ILogger<VoertuigenController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllVoertuigen")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VoertuigOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<VoertuigOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var voertuigen = await _repository.GetAllAsync();
                var voertuigenDTO = _mapper.Map<List<VoertuigOutgoingDTO>>(voertuigen);
                return Ok(voertuigenDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigenController: GetAllAsync " + ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetVoertuigById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VoertuigOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VoertuigOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var voertuig = await _repository.GetByIdAsync(id);
                var voertuigDTO = _mapper.Map<VoertuigOutgoingDTO>(voertuig);
                return Ok(voertuigDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigenController: GetByIdAsync" + ex.Message, ex);
                if (ex is EntityDoesNotExistException) return BadRequest(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost(Name = "CreateVoertuig")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VoertuigOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([Required][FromBody] VoertuigIncomingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var newVoertuig = await _repository.InsertAsync(_mapper.Map<DVoertuig>(dto));
                return CreatedAtAction(nameof(GetByIdAsync), new { id = newVoertuig.VoertuigId }, _mapper.Map<VoertuigOutgoingDTO>(newVoertuig));
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigenController: PostAsync" + ex.Message, ex);
                if (ex is EntityAlreadyExistsException || ex is VoertuigException)
                    return BadRequest(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut(Name = "UpdateVoertuig")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([Required][FromBody] VoertuigUpdateIncomingDTO dto) 
        {
            if (!ModelState.IsValid)
            {
                _logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                await _repository.UpdateAsync(_mapper.Map<DVoertuig>(dto));
                return Ok("Resource updatet");
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigenController: PutAsync" + ex.Message, ex);
                if (ex is VoertuigException || ex is EntityDoesNotExistException) 
                    return BadRequest(ex.Message);
                return StatusCode(500, ex.Message); 
            }
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteVoertuig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([Required] int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("VoertuigController: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) 
                    return BadRequest(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


    }
    
}
