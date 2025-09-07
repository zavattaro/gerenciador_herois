using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Interface genérica para operações básicas de repositório
/// </summary>
/// <typeparam name="T">Tipo da entidade</typeparam>
/// <remarks>
/// Define o contrato padrão para operações CRUD (Create, Read, Update, Delete)
/// que podem ser reutilizadas por diferentes repositórios da aplicação
/// </remarks>
namespace HeroesAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
