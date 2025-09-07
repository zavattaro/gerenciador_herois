using HeroesAPI.Models;

/// <summary>
/// Interface para operações de repositório de superpoderes
/// </summary>
/// <remarks>
/// Define o contrato para operações CRUD de superpoderes,
/// permitindo o gerenciamento do catálogo de habilidades especiais
/// </remarks>
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