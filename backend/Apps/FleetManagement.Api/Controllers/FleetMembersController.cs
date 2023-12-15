using AutoMapper;
using FleetManagement.Api.DTOs.FleetMember;
using FleetManagement.Api.Extensions;
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
    [Route("fleet")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class FleetMembersController : ControllerBase
    {
        private readonly IFleetMemberRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<FleetMembersController> _logger;

        public FleetMembersController(IFleetMemberRepository repository, IMapper mapper, ILogger<FleetMembersController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllFleetMembers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FleetMemberOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<FleetMemberOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var fleetMembers = await _repository.GetAllAsync();
                var fleetMembersDTO = _mapper.Map<List<FleetMemberOutgoingDTO>>(fleetMembers);
                return Ok(fleetMembersDTO);
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogError("FleetMembersController: GetAllAsync: " + ex.Message, ex);
                return StatusCode(500, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMembersController: GetAllAsync: " + ex.Message, ex);
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetFleetMemberById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FleetMemberOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FleetMemberOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var fleetMember = await _repository.GetByIdAsync(id);
                var fleetMemberDTO = _mapper.Map<FleetMemberOutgoingDTO>(fleetMember);
                return Ok(fleetMemberDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMembersController: GetByIdAsync: " + ex.Message, ex);
                if(ex is EntityDoesNotExistException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpPut(Name = "UpdateFleetMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([Required][FromBody] FleetMemberUpdateIdsIncomingDTO updateMember)
        {
            if (!ModelState.IsValid) //Checks if the dto adheres to the data-annotations specified for a BestuurderIncomingDTO
            {
                _logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                await _repository.UpdateAsync(_mapper.Map<DFleetMemberIds>(updateMember));
                return Ok("FleetMember updated succesfully");
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMembersController: PutAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException || ex is EntityNotAvailableException || ex is EntityAlreadyExistsException)
                {
                    return BadRequest(ex.Message);
                }
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize]
        [HttpPost(Name = "InsertFleetMember")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([Required][FromBody] FleetMemberIdsNewIncompingDTO newMember)
        {
            if (!ModelState.IsValid) //Checks if the dto adheres to the data-annotations specified for a BestuurderIncomingDTO
            {
                _logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var insertedMember = await _repository.InsertAsync(_mapper.Map<DFleetMemberIds>(newMember));
                return CreatedAtAction(nameof(GetByIdAsync), "FleetMembers", new { id = insertedMember.FleetId}, _mapper.Map<FleetMemberOutgoingDTO>(insertedMember));
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMembersController: PutAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException || ex is EntityNotAvailableException || ex is EntityAlreadyExistsException)
                {
                    return BadRequest(ex.Message);
                }
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteFleetMemberById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByIdAsync([Required] int id)
        {
            try
            {
                await _repository.DeleteByIdAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("FleetMembersController: DeleteByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException)
                {
                    return BadRequest(ex.Message);
                }
                return StatusCode(500, ex.Message);
            }

        }
    }
}