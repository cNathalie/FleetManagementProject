using AutoMapper;
using FleetManagement.Api.DTOs.BrandstofType;
using FM.Domain.Interfaces;
using FM.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FleetManagement.Api.Controllers
{

    [ApiController]
    [Route("brandstoftypes")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class BrandstofTypesController : ControllerBase
    {
        private readonly IBrandstofTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandstofTypesController> _logger;
        public BrandstofTypesController(IBrandstofTypeRepository repository, IMapper mapper, ILogger<BrandstofTypesController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllFuelsAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BrandstofTypeOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<BrandstofTypeOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var brandstoffen = await _repository.GetAllAsync();
                return Ok(_mapper.Map<List<BrandstofTypeOutgoingDTO>>(brandstoffen));
            }
            catch (Exception ex)
            {
                _logger.LogError("BrandstofTypesController: GetAllAsync: " + ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetFuelByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandstofTypeOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BrandstofTypeOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var brandstof = await _repository.GetByIdAsync(id);
                return Ok(_mapper.Map<BrandstofTypeOutgoingDTO>(brandstof));
            }
            catch (Exception ex)
            {
                _logger.LogError("BrandstofTypesController: GetAllAsync: " + ex.Message, ex);
                if (ex is EntityDoesNotExistException) return BadRequest(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
