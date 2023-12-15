using AutoMapper;
using FleetManagement.Api.DTOs.TypeWagen;
using FM.Domain.Interfaces;
using FM.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FleetManagement.Api.Controllers
{
    [ApiController]
    [Route("typeswagen")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TypesWagenController : ControllerBase
    {
        private readonly ITypeWagenRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TypesWagenController> _logger;

        public TypesWagenController(ITypeWagenRepository repository, IMapper mapper, ILogger<TypesWagenController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllTypes")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TypeWagenOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TypeWagenOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var types = await _repository.GetAllAsync();
                return Ok(_mapper.Map<List<TypeWagenOutgoingDTO>>(types));
            }
            catch (Exception ex)
            {
                _logger.LogError("TypesWagenController: GetAllAsync" + ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetTypeById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeWagenOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeWagenOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var type = await _repository.GetByIdAsync(id);
                var typeDTO = _mapper.Map<TypeWagenOutgoingDTO>(type);
                return Ok(typeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("TypesWagenController: GetByIdAsync" + ex.Message, ex);
                if (ex is EntityDoesNotExistException) return BadRequest(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
