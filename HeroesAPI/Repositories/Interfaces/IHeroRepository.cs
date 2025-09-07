using HeroesAPI.DTOs;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;

/// <summary>
/// Interface específica para operações de repositório de heróis
/// </summary>
/// <remarks>
/// Estende a interface genérica IRepository com métodos especializados
/// para gerenciamento de heróis e suas relações com superpoderes
/// </remarks>
public interface IHeroRepository : IRepository<Hero>
{
    Task<Hero?> GetByHeroNameAsync(string heroName);
    Task<bool> HeroNameExistsAsync(string heroName);
    Task<IEnumerable<HeroWithSuperpowersDto>> GetHeroesWithSuperpowersAsync();
    Task<HeroWithSuperpowersDto?> GetHeroWithSuperpowersByIdAsync(int id);
    Task<bool> SuperpowerExistsAsync(int superpowerId);
    Task<Superpower?> GetSuperpowerByIdAsync(int superpowerId);
    Task<Hero> AddAsync(Hero hero, List<int> superpowerIds);
    Task UpdateAsync(Hero hero, List<int> superpowerIds);
}