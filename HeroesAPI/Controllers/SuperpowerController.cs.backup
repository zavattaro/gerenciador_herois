using HeroesAPI.Models;
using HeroesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HeroesAPI.Controllers
{
    /// <summary>
    /// Controlador para gerenciamento de superpoderes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SuperpowersController : ControllerBase
    {
        private readonly ISuperpowerService _superpowerService;

        public SuperpowersController(ISuperpowerService superpowerService)
        {
            _superpowerService = superpowerService;
        }

        /// <summary>
        /// Obtém todos os superpoderes disponíveis
        /// </summary>
        /// <returns>Lista de superpoderes</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Obter todos os superpoderes", Description = "Retorna todos os superpoderes cadastrados no sistema")]
        [SwaggerResponse(200, "Lista de superpoderes obtida com sucesso", typeof(IEnumerable<Superpower>))]
        [SwaggerResponse(500, "Erro interno do servidor")]
        public async Task<ActionResult<IEnumerable<Superpower>>> GetAllSuperpowers()
        {
            var superpowers = await _superpowerService.GetAllSuperpowersAsync();
            return Ok(superpowers);
        }

        /// <summary>
        /// Obtém um superpoder específico pelo ID
        /// </summary>
        /// <param name="id">ID do superpoder</param>
        /// <returns>Superpoder encontrado</returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obter superpoder por ID", Description = "Retorna um superpoder específico pelo seu identificador")]
        [SwaggerResponse(200, "Superpoder encontrado", typeof(Superpower))]
        [SwaggerResponse(404, "Superpoder não encontrado")]
        [SwaggerResponse(500, "Erro interno do servidor")]
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