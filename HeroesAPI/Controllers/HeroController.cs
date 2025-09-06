using Microsoft.AspNetCore.Mvc;
using HeroesAPI.Models;
using HeroesAPI.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HeroesAPI.Services;
using HeroesAPI.DTOs;

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

            var hero = await _heroService.GetHeroByIdAsync(id);
            if (hero == null)
            {
                _logger.LogWarning("Hero with ID: {HeroId} not found", id);
                return NotFound();
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            _logger.LogInformation("Creating new hero: {HeroName}", hero.HeroName);

            try
            {
                var createdHero = await _heroService.CreateHeroAsync(hero);
                _logger.LogInformation("Hero created with ID: {HeroId}", createdHero.Id);
                return CreatedAtAction(nameof(GetHero), new { id = createdHero.Id }, createdHero);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error creating hero");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, Hero hero)
        {
            _logger.LogInformation("Updating hero with ID: {HeroId}", id);

            if (id != hero.Id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                await _heroService.UpdateHeroAsync(id, hero);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error updating hero with ID: {HeroId}", id);
                return BadRequest(ex.Message);
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
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error deleting hero with ID: {HeroId}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}