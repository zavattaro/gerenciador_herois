using Microsoft.AspNetCore.Mvc;
using HeroesAPI.Models;
using HeroesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HeroesAPI.DTOs;
using HeroesAPI.Dtos;

namespace HeroesAPI.Controllers
{
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HeroWithSuperpowersDto>>> GetHeroes()
        {
            _logger.LogInformation("Getting all heroes with superpowers");
            var heroes = await _heroService.GetAllHeroesAsync();
            return Ok(heroes);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

                // ✅ Buscar o herói atualizado para retornar
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

        [HttpDelete("{id}")]
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