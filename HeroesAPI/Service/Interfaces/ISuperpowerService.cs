using HeroesAPI.Models;

namespace HeroesAPI.Services
{
    public interface ISuperpowerService
    {
        Task<IEnumerable<Superpower>> GetAllSuperpowersAsync();
        Task<Superpower?> GetSuperpowerByIdAsync(int id);
    }
}