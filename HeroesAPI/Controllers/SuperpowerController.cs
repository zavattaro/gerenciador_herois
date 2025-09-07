using HeroesAPI.Models;
using HeroesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HeroesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperpowersController : ControllerBase
    {
        private readonly ISuperpowerService _superpowerService;

        public SuperpowersController(ISuperpowerService superpowerService)
        {
            _superpowerService = superpowerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Superpower>>> GetAllSuperpowers()
        {
            var superpowers = await _superpowerService.GetAllSuperpowersAsync();
            return Ok(superpowers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Superpower>> GetSuperpowerById(int id)
        {
            var superpower = await _superpowerService.GetSuperpowerByIdAsync(id);

            if (superpower == null)
            {
                return NotFound();
            }

            return Ok(superpower);
        }
    }
}