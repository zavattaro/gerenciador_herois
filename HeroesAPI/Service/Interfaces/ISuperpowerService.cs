using HeroesAPI.Models;

/// <summary>
/// Interface para serviços de gerenciamento de superpoderes
/// </summary>
namespace HeroesAPI.Services
{
    public interface ISuperpowerService
    {
        Task<IEnumerable<Superpower>> GetAllSuperpowersAsync();
        Task<Superpower?> GetSuperpowerByIdAsync(int id);
    }
}