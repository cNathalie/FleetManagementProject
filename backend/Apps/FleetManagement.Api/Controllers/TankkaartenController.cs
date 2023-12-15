using AutoMapper;
using FleetManagement.Api.DTOs.Bestuurder;
using FleetManagement.Api.DTOs.Tankkaart;
using FleetManagement.Api.Extensions;
using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FleetManagement.Api.Controllers
{
    [ApiController]
    [Route("tankkaarten")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TankkaartenController : ControllerBase
    {
        private readonly ITankkaartRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TankkaartenController> _logger;

        public TankkaartenController(ITankkaartRepository repository, IMapper mapper, ILogger<TankkaartenController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllTankkaarten")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TankkaartOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TankkaartOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var tankkaarten = await _repository.GetAllAsync();
                var tankkaartenDTO = _mapper.Map<List<TankkaartOutgoingDTO>>(tankkaarten);
                return Ok(tankkaartenDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartenController: GetAllAsync: " + ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetTankkaartById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TankkaartOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TankkaartOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var tankkaart = await _repository.GetByIdAsync(id);
                var tankaartDTO = _mapper.Map<TankkaartOutgoingDTO>(tankkaart);
                return Ok(tankaartDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartController: GetByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpPost(Name = "CreateTankkaart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([Required][FromBody] TankkaartNewIncomingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var createdTankkaart = await _repository.InsertAsync(_mapper.Map<DTankkaart>(dto));
                return CreatedAtAction(nameof(GetByIdAsync), new { id = createdTankkaart.TankkaartId }, _mapper.Map<TankkaartOutgoingDTO>(createdTankkaart));
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartController: InsertAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException
                    || ex is DateTimeInThePastException
                    || ex is EntityDoesNotExistException)
                    return BadRequest(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpPut(Name = "UpdateTankkaart")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([Required][FromBody] TankkaartUpdateIncomingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var updateTankkaart = _mapper.Map<DTankkaart>(dto);
                await _repository.UpdateAsync(updateTankkaart);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = dto.TankkaartId }, _mapper.Map<TankkaartOutgoingDTO>(updateTankkaart));
            }
            catch (Exception ex)
            {
                _logger.LogError("TankkaartController: UpdateAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException
                    || ex is DateTimeInThePastException
                    || ex is EntityDoesNotExistException
                    || ex is ActiveCardException)
                    return BadRequest(ex.Message);
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteTankkaartById")]
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
                _logger.LogError("TankkaartRepo: UpdateAsync: " + ex.Message, ex);
                if (ex is EntityAlreadyExistsException || ex is EntityDoesNotExistException
                    || ex is EntityNotAvailableException || ex is OperationCanceledException
                    || ex is DbUpdateException || ex is DateTimeInThePastException
                    || ex is ActiveCardException) return BadRequest(ex.Message);
                return StatusCode(500, ex);
            }

        }
    }
}