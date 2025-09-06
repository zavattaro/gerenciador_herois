using HeroesAPI.DTOs;
using HeroesAPI.Models;

namespace HeroesAPI.Repositories.Interfaces
{
    public interface IHeroRepository : IRepository<Hero>
    {
        Task<Hero?> GetByHeroNameAsync(string heroName);
        Task<bool> HeroNameExistsAsync(string heroName);
        Task<IEnumerable<HeroWithSuperpowersDto>> GetHeroesWithSuperpowersAsync();
        Task<HeroWithSuperpowersDto?> GetHeroWithSuperpowersByIdAsync(int id);
    }
}
