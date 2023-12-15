using AutoMapper;
using FM.Infrastructure.Exceptions;
using FM.Domain.Interfaces;
using FM.Domain.Models;
using FM.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Mime;
using FleetManagement.Api.DTOs.Bestuurder;
using FleetManagement.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using FleetManagement.Api.MediatR.Queries;
using FleetManagement.Api.MediatR.Commands;

namespace FleetManagement.Api.Controllers
{
    [ApiController]
    [Route("bestuurders")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BestuurdersController : ControllerBase
    {
        private readonly IBestuurderRepository _repository;
        private readonly IMapper _mapper;
        //private readonly ILogger<BestuurdersController> _logger;
        private readonly IMediator _mediator;
        public BestuurdersController(IBestuurderRepository repository, IMapper mapper, /*ILogger<BestuurdersController> logger,*/ IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            //_logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet(Name = "GetAllBestuurders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BestuurderOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<BestuurderOutgoingDTO>>> GetAllAsync()
        {
            var timer = new Stopwatch();                                                                //Timing: WimDelvaux special request
            timer.Start();
            try
            {
                var bestuurders = await _mediator.Send(new GetBestuurdersQuery());
                var bestuurdersDTO = _mapper.Map<List<BestuurderOutgoingDTO>>(bestuurders);
                return Ok(bestuurdersDTO);
            }
            catch (Exception ex)
            {
                //_logger.LogError("BestuurderController: GetAllAsync: " + ex.Message, ex);
                return BadRequest(ex);
            }
            finally
            {
                timer.Stop();
                //_logger.LogInformation($"TIMER BestuurderController-GetAllAsync executed in: {timer.Elapsed}");
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetBestuurderById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BestuurderOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BestuurderOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var bestuurder = await _mediator.Send(new GetBestuurderByIdQuery(id));
                var bestuurderDTO = _mapper.Map<BestuurderOutgoingDTO>(bestuurder);
                return Ok(bestuurderDTO);
            }
            catch (Exception ex)
            {
                //_logger.LogError("BestuurdersController: GetByIdAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpPost(Name = "CreateBestuurder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync([Required][FromBody] BestuurderIncomingDTO dto)
        {
            if (!ModelState.IsValid) //Checks if the dto adheres to the data-annotations specified for a BestuurderIncomingDTO
            {
                //_logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var newBestuurder = await _mediator.Send(new CreateBestuurderCommand(_mapper.Map<DBestuurder>(dto)));
                return CreatedAtAction(nameof(GetByIdAsync), new { id = newBestuurder.BestuurderId }, _mapper.Map<BestuurderOutgoingDTO>(newBestuurder));
            }
            catch (Exception ex)
            {
                //_logger.LogError("BestuurderController: PostAsync: " + ex.Message, ex);
                if(ex is EntityAlreadyExistsException || ex is RRNException) 
                    return BadRequest(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut(Name = "UpdateBestuurder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync([Required][FromBody] BestuurderIncomingUpdateDTO bestuurderDTO)
        {
            if (!ModelState.IsValid)
            {
                //_logger.LogModelStateErrors(ModelState);
                return BadRequest(ModelState);
            }
            try
            {
                var updateBestuuder = _mapper.Map<DBestuurder>(bestuurderDTO);
                await _mediator.Send(new UpdateBestuurderCommand(updateBestuuder));
                return CreatedAtAction(nameof(GetByIdAsync), new { id = bestuurderDTO.BestuurderId }, _mapper.Map<BestuurderOutgoingDTO>(updateBestuuder));
            }
            catch (Exception ex)
            {
                //_logger.LogError("BestuurderController: PostAsync: " + ex.Message, ex);
                return BadRequest(ex.Message);
            }

        }

        [Authorize]
        [HttpDelete("{id}", Name = "DeleteBestuurderById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteByIdAsync([Required] int id)
        {
            try
            {
                await _mediator.Send(new DeleteBestuurderCommand(id));
                //_logger.LogInformation($"Bestuurder-entity with {id} deleted from database");
                return NoContent();
            }
            catch (Exception ex)
            {
                //_logger.LogError("BestuurderController: DeleteAsync: " + ex.Message, ex);
                return BadRequest(ex);
            }
        }
    }
}
