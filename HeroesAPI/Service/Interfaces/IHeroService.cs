using HeroesAPI.Dtos;
using HeroesAPI.DTOs;

/// <summary>
/// Interface para serviços de gerenciamento de heróis
/// </summary>
namespace HeroesAPI.Services
{
    public interface IHeroService
    {
        Task<IEnumerable<HeroWithSuperpowersDto>> GetAllHeroesAsync();
        Task<HeroWithSuperpowersDto?> GetHeroByIdAsync(int id);
        Task<HeroWithSuperpowersDto> CreateHeroAsync(CreateHeroRequestDto createHeroRequest); // ← Deve ser assim
        Task UpdateHeroAsync(int id, UpdateHeroRequestDto updateHeroRequest);
        Task DeleteHeroAsync(int id);
        Task<bool> HeroExistsAsync(int id);
    }
}