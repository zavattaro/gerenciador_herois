using HeroesAPI.Data;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Repositories
{
    /// <summary>
    /// Repositório para operações de banco de dados relacionadas a superpoderes
    /// </summary>
    public class SuperpowerRepository : ISuperpowerRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do repositório de superpoderes
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public SuperpowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém todos os superpoderes ordenados por nome
        /// </summary>
        /// <returns>Lista de superpoderes ordenada alfabeticamente</returns>
        public async Task<IEnumerable<Superpower>> GetAllAsync()
        {
            return await _context.Superpowers
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Obtém um superpoder pelo ID
        /// </summary>
        /// <param name="id">ID do superpoder</param>
        /// <returns>Superpoder encontrado ou null</returns>
        public async Task<Superpower?> GetByIdAsync(int id)
        {
            return await _context.Superpowers.FindAsync(id);
        }

        /// <summary>
        /// Verifica se um superpoder existe
        /// </summary>
        /// <param name="id">ID do superpoder</param>
        /// <returns>True se o superpoder existe, False caso contrário</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Superpowers.AnyAsync(s => s.Id == id);
        }

        /// <summary>
        /// Adiciona um novo superpoder
        /// </summary>
        /// <param name="superpower">Superpoder a ser adicionado</param>
        /// <returns>Superpoder criado</returns>
        public async Task<Superpower> AddAsync(Superpower superpower)
        {
            await _context.Superpowers.AddAsync(superpower);
            await _context.SaveChangesAsync();
            return superpower;
        }

        /// <summary>
        /// Atualiza um superpoder existente
        /// </summary>
        /// <param name="superpower">Superpoder com dados atualizados</param>
        public async Task UpdateAsync(Superpower superpower)
        {
            _context.Superpowers.Update(superpower);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exclui um superpoder pelo ID
        /// </summary>
        /// <param name="id">ID do superpoder</param>
        public async Task DeleteAsync(int id)
        {
            var superpower = await GetByIdAsync(id);
            if (superpower != null)
            {
                _context.Superpowers.Remove(superpower);
                await _context.SaveChangesAsync();
            }
        }
    }
}