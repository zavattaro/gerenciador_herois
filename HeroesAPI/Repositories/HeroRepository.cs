using HeroesAPI.Data;
using HeroesAPI.DTOs;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Repositories
{
    /// <summary>
    /// Repositório para operações de banco de dados relacionadas a heróis
    /// </summary>
    public class HeroRepository : IHeroRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor do repositório de heróis
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        public HeroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adiciona um herói sem superpoderes
        /// </summary>
        /// <param name="hero">Herói a ser adicionado</param>
        /// <returns>Herói criado</returns>
        public async Task<Hero> AddAsync(Hero hero)
        {
            return await AddAsync(hero, new List<int>());
        }

        /// <summary>
        /// Adiciona um herói com superpoderes
        /// </summary>
        /// <param name="hero">Herói a ser adicionado</param>
        /// <param name="superpowerIds">IDs dos superpoderes a serem vinculados</param>
        /// <returns>Herói criado com superpoderes</returns>
        public async Task<Hero> AddAsync(Hero hero, List<int> superpowerIds)
        {
            try
            {
                hero.BirthDate = hero.BirthDate.ToUniversalTime();
                hero.CreatedAt = DateTime.UtcNow;

                // Vincular superpoderes
                foreach (var superpowerId in superpowerIds)
                {
                    var superpower = await _context.Superpowers.FindAsync(superpowerId);
                    if (superpower != null)
                    {
                        hero.HeroSuperpowers.Add(new HeroSuperpower
                        {
                            Hero = hero,
                            Superpower = superpower
                        });
                    }
                }

                await _context.Heroes.AddAsync(hero);
                await _context.SaveChangesAsync();

                return hero;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar herói: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtém todos os heróis (sem superpoderes)
        /// </summary>
        /// <returns>Lista de todos os heróis</returns>
        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            return await _context.Heroes.ToListAsync();
        }

        /// <summary>
        /// Obtém um herói pelo ID (sem superpoderes)
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>Herói encontrado ou null</returns>
        public async Task<Hero?> GetByIdAsync(int id)
        {
            return await _context.Heroes.FindAsync(id);
        }

        /// <summary>
        /// Obtém um herói pelo nome
        /// </summary>
        /// <param name="heroName">Nome do herói</param>
        /// <returns>Herói encontrado ou null</returns>
        public async Task<Hero?> GetByHeroNameAsync(string heroName)
        {
            return await _context.Heroes
                .FirstOrDefaultAsync(h => h.HeroName == heroName);
        }

        /// <summary>
        /// Verifica se um nome de herói já existe
        /// </summary>
        /// <param name="heroName">Nome do herói</param>
        /// <returns>True se o nome já existe, False caso contrário</returns>
        public async Task<bool> HeroNameExistsAsync(string heroName)
        {
            return await _context.Heroes
                .AnyAsync(h => h.HeroName == heroName);
        }

        /// <summary>
        /// Atualiza um herói (sem alterar superpoderes)
        /// </summary>
        /// <param name="hero">Herói a ser atualizado</param>
        public async Task UpdateAsync(Hero hero)
        {
            _context.Heroes.Update(hero);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Exclui um herói pelo ID
        /// </summary>
        /// <param name="id">ID do herói</param>
        public async Task DeleteAsync(int id)
        {
            var hero = await GetByIdAsync(id);
            if (hero != null)
            {
                _context.Heroes.Remove(hero);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Verifica se um herói existe
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>True se o herói existe, False caso contrário</returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Heroes.AnyAsync(e => e.Id == id);
        }

        /// <summary>
        /// Obtém todos os heróis com seus superpoderes
        /// </summary>
        /// <returns>Lista de heróis com superpoderes</returns>
        public async Task<IEnumerable<HeroWithSuperpowersDto>> GetHeroesWithSuperpowersAsync()
        {
            return await _context.Heroes
                .Include(h => h.HeroSuperpowers)
                .ThenInclude(hs => hs.Superpower)
                .Select(h => new HeroWithSuperpowersDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    HeroName = h.HeroName,
                    BirthDate = h.BirthDate,
                    Height = h.Height,
                    Weight = h.Weight,
                    CreatedAt = h.CreatedAt,
                    Superpowers = h.HeroSuperpowers.Select(hs => new SuperpowerDto
                    {
                        Id = hs.Superpower.Id,
                        Name = hs.Superpower.Name,
                        Description = hs.Superpower.Description
                    }).ToList()
                })
                .ToListAsync();
        }

        /// <summary>
        /// Obtém um herói específico com seus superpoderes
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>Herói com superpoderes ou null</returns>
        public async Task<HeroWithSuperpowersDto?> GetHeroWithSuperpowersByIdAsync(int id)
        {
            return await _context.Heroes
                .Where(h => h.Id == id)
                .Include(h => h.HeroSuperpowers)
                .ThenInclude(hs => hs.Superpower)
                .Select(h => new HeroWithSuperpowersDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    HeroName = h.HeroName,
                    BirthDate = h.BirthDate,
                    Height = h.Height,
                    Weight = h.Weight,
                    CreatedAt = h.CreatedAt,
                    Superpowers = h.HeroSuperpowers.Select(hs => new SuperpowerDto
                    {
                        Id = hs.Superpower.Id,
                        Name = hs.Superpower.Name,
                        Description = hs.Superpower.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Verifica se um superpoder existe
        /// </summary>
        /// <param name="superpowerId">ID do superpoder</param>
        /// <returns>True se o superpoder existe, False caso contrário</returns>
        public async Task<bool> SuperpowerExistsAsync(int superpowerId)
        {
            return await _context.Superpowers.AnyAsync(s => s.Id == superpowerId);
        }

        /// <summary>
        /// Obtém um superpoder pelo ID
        /// </summary>
        /// <param name="superpowerId">ID do superpoder</param>
        /// <returns>Superpoder encontrado ou null</returns>
        public async Task<Superpower?> GetSuperpowerByIdAsync(int superpowerId)
        {
            return await _context.Superpowers.FindAsync(superpowerId);
        }

        /// <summary>
        /// Atualiza um herói e seus superpoderes
        /// </summary>
        /// <param name="hero">Herói a ser atualizado</param>
        /// <param name="superpowerIds">Novos IDs dos superpoderes</param>
        public async Task UpdateAsync(Hero hero, List<int> superpowerIds)
        {
            // Remover superpoderes existentes
            var existingSuperpowers = _context.HeroSuperpowers
                .Where(hs => hs.HeroId == hero.Id);
            _context.HeroSuperpowers.RemoveRange(existingSuperpowers);

            // Adicionar novos superpoderes
            foreach (var superpowerId in superpowerIds)
            {
                var superpower = await GetSuperpowerByIdAsync(superpowerId);
                if (superpower != null)
                {
                    hero.HeroSuperpowers.Add(new HeroSuperpower
                    {
                        Hero = hero,
                        Superpower = superpower
                    });
                }
            }

            _context.Heroes.Update(hero);
            await _context.SaveChangesAsync();
        }
    }
}