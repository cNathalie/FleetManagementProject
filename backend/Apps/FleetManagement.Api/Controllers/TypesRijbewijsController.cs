using AutoMapper;
using FleetManagement.Api.DTOs.BrandstofType;
using FleetManagement.Api.DTOs.TypeRijbewijs;
using FM.Domain.Interfaces;
using FM.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace FleetManagement.Api.Controllers
{
    [ApiController]
    [Route("typesrijbewijs")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]

    public class TypesRijbewijsController : ControllerBase
    {
        private readonly ITypeRijbewijsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<TypesRijbewijsController> _logger;

        public TypesRijbewijsController(ITypeRijbewijsRepository repository, IMapper mapper, ILogger<TypesRijbewijsController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet(Name = "GetAllRijbewijzenAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TypeRijbewijsOutgoingDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TypeRijbewijsOutgoingDTO>>> GetAllAsync()
        {
            try
            {
                var rijbewijzen = await _repository.GetAllAsync();
                var rijbewijzenDTO = _mapper.Map<List<TypeRijbewijsOutgoingDTO>>(rijbewijzen);
                return Ok(rijbewijzenDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("TypesRijbewijsController: GetAllAsync: " + ex.Message, ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetBrandstofByIdAsync")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TypeRijbewijsOutgoingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TypeRijbewijsOutgoingDTO>> GetByIdAsync([Required] int id)
        {
            try
            {
                var rijbewijs = await _repository.GetByIdAsync(id);
                var rijbewijsDTO = _mapper.Map<TypeRijbewijsOutgoingDTO>(rijbewijs);
                return Ok(rijbewijsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError("TypesRijbewijsController: GetAllAsync: " + ex.Message, ex);
                if(ex is EntityDoesNotExistException) return BadRequest(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}