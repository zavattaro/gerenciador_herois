using HeroesAPI.Models;

namespace HeroesAPI.Repositories.Interfaces
{
    public interface ISuperpowerRepository
    {
        Task<IEnumerable<Superpower>> GetAllAsync();
        Task<Superpower?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Superpower> AddAsync(Superpower superpower);
        Task UpdateAsync(Superpower superpower);
        Task DeleteAsync(int id);
    }
}