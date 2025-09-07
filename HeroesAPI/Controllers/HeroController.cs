using Microsoft.AspNetCore.Mvc;
using HeroesAPI.Models;
using HeroesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HeroesAPI.DTOs;
using HeroesAPI.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace HeroesAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciamento de heróis
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;
        private readonly ILogger<HeroController> _logger;

        public HeroController(IHeroService heroService, ILogger<HeroController> logger)
        {
            _heroService = heroService;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os heróis com seus superpoderes
        /// </summary>
        /// <returns>Lista de heróis</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os heróis", Description = "Retorna todos os heróis cadastrados com seus superpoderes")]
        [SwaggerResponse(200, "Lista de heróis obtida com sucesso", typeof(IEnumerable<HeroWithSuperpowersDto>))]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<IEnumerable<HeroWithSuperpowersDto>>> GetHeroes()
        {
            _logger.LogInformation("Getting all heroes with superpowers");
            var heroes = await _heroService.GetAllHeroesAsync();
            return Ok(heroes);
        }

        /// <summary>
        /// Obtém um herói específico pelo ID
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>Herói encontrado</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter herói por ID", Description = "Retorna um herói específico pelo seu identificador")]
        [SwaggerResponse(200, "Herói encontrado", typeof(HeroWithSuperpowersDto))]
        [SwaggerResponse(404, "Herói não encontrado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<HeroWithSuperpowersDto>> GetHero(int id)
        {
            _logger.LogInformation("Getting hero with ID: {HeroId}", id);

            try
            {
                var hero = await _heroService.GetHeroByIdAsync(id);
                return Ok(hero);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Hero with ID: {HeroId} not found", id);
                return NotFound(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cria um novo herói
        /// </summary>
        /// <param name="createHeroRequest">Dados do herói a ser criado</param>
        /// <returns>Herói criado</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Criar novo herói", Description = "Cria um novo herói com os dados fornecidos")]
        [SwaggerResponse(201, "Herói criado com sucesso", typeof(HeroWithSuperpowersDto))]
        [SwaggerResponse(400, "Dados inválidos ou nome de herói já existe")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<HeroWithSuperpowersDto>> PostHero([FromBody] CreateHeroRequestDto createHeroRequest)
        {
            _logger.LogInformation("Creating new hero: {HeroName}", createHeroRequest.HeroName);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdHero = await _heroService.CreateHeroAsync(createHeroRequest);
                _logger.LogInformation("Hero created with ID: {HeroId}", createdHero.Id);
                return CreatedAtAction(nameof(GetHero), new { id = createdHero.Id }, createdHero);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Validation error creating hero: {Error}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating hero");
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Atualiza um herói existente
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <param name="updateHeroRequest">Dados atualizados do herói</param>
        /// <returns>Herói atualizado</returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualizar herói", Description = "Atualiza os dados de um herói existente")]
        [SwaggerResponse(200, "Herói atualizado com sucesso", typeof(HeroWithSuperpowersDto))]
        [SwaggerResponse(400, "Dados inválidos ou ID mismatch")]
        [SwaggerResponse(404, "Herói não encontrado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<HeroWithSuperpowersDto>> PutHero(int id, [FromBody] UpdateHeroRequestDto updateHeroRequest)
        {
            _logger.LogInformation("Updating hero with ID: {HeroId}", id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateHeroRequest.Id)
            {
                return BadRequest(new { message = "ID mismatch" });
            }

            try
            {
                await _heroService.UpdateHeroAsync(id, updateHeroRequest);
                var updatedHero = await _heroService.GetHeroByIdAsync(id);
                return Ok(updatedHero);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Hero with ID: {HeroId} not found for update", id);
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Validation error updating hero: {Error}", ex.Message);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating hero with ID: {HeroId}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        /// <summary>
        /// Exclui um herói
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>Status da operação</returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Excluir herói", Description = "Remove um herói do sistema")]
        [SwaggerResponse(204, "Herói excluído com sucesso")]
        [SwaggerResponse(404, "Herói não encontrado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            _logger.LogInformation("Deleting hero with ID: {HeroId}", id);

            try
            {
                await _heroService.DeleteHeroAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Hero with ID: {HeroId} not found for deletion", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting hero with ID: {HeroId}", id);
                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}