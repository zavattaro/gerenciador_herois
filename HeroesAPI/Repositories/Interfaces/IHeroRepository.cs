using HeroesAPI.DTOs;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;

public interface IHeroRepository : IRepository<Hero>
{
    // Métodos específicos para heróis
    Task<Hero?> GetByHeroNameAsync(string heroName);
    Task<bool> HeroNameExistsAsync(string heroName);
    Task<IEnumerable<HeroWithSuperpowersDto>> GetHeroesWithSuperpowersAsync();
    Task<HeroWithSuperpowersDto?> GetHeroWithSuperpowersByIdAsync(int id);
    Task<bool> SuperpowerExistsAsync(int superpowerId);
    Task<Superpower?> GetSuperpowerByIdAsync(int superpowerId);
    Task<Hero> AddAsync(Hero hero, List<int> superpowerIds);
    Task UpdateAsync(Hero hero, List<int> superpowerIds);
}