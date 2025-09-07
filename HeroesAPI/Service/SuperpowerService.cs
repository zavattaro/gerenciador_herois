using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;

namespace HeroesAPI.Services
{
    /// <summary>
    /// Serviço para gerenciamento de superpoderes
    /// </summary>
    public class SuperpowerService : ISuperpowerService
    {
        private readonly ISuperpowerRepository _superpowerRepository;

        /// <summary>
        /// Construtor do serviço de superpoderes
        /// </summary>
        /// <param name="superpowerRepository">Repositório de superpoderes</param>
        public SuperpowerService(ISuperpowerRepository superpowerRepository)
        {
            _superpowerRepository = superpowerRepository;
        }

        /// <summary>
        /// Obtém todos os superpoderes disponíveis
        /// </summary>
        /// <returns>Lista de superpoderes</returns>
        public async Task<IEnumerable<Superpower>> GetAllSuperpowersAsync()
        {
            return await _superpowerRepository.GetAllAsync();
        }

        /// <summary>
        /// Obtém um superpoder específico pelo ID
        /// </summary>
        /// <param name="id">ID do superpoder</param>
        /// <returns>Superpoder encontrado ou null</returns>
        public async Task<Superpower?> GetSuperpowerByIdAsync(int id)
        {
            return await _superpowerRepository.GetByIdAsync(id);
        }
    }
}