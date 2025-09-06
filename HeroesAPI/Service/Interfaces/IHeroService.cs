using HeroesAPI.DTOs;
using HeroesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesAPI.Services
{
    public interface IHeroService
    {
        Task<IEnumerable<HeroWithSuperpowersDto>> GetAllHeroesAsync();
        Task<HeroWithSuperpowersDto?> GetHeroByIdAsync(int id);
        Task<Hero> CreateHeroAsync(Hero hero);
        Task UpdateHeroAsync(int id, Hero hero);
        Task DeleteHeroAsync(int id);
        Task<bool> HeroExistsAsync(int id);
    }
}
