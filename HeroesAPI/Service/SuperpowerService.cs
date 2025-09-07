using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;

namespace HeroesAPI.Services
{
    public class SuperpowerService : ISuperpowerService
    {
        private readonly ISuperpowerRepository _superpowerRepository;

        public SuperpowerService(ISuperpowerRepository superpowerRepository)
        {
            _superpowerRepository = superpowerRepository;
        }

        public async Task<IEnumerable<Superpower>> GetAllSuperpowersAsync()
        {
            return await _superpowerRepository.GetAllAsync();
        }

        public async Task<Superpower?> GetSuperpowerByIdAsync(int id)
        {
            return await _superpowerRepository.GetByIdAsync(id);
        }
    }
}