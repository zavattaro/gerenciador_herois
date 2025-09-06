using HeroesAPI.Data;
using HeroesAPI.DTOs;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Repositories
{
    public class HeroRepository : IHeroRepository
    {
        private readonly ApplicationDbContext _context;

        public HeroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Hero>> GetAllAsync()
        {
            return await _context.Heroes.ToListAsync();
        }

        public async Task<Hero?> GetByIdAsync(int id)
        {
            return await _context.Heroes.FindAsync(id);
        }

        public async Task<Hero?> GetByHeroNameAsync(string heroName)
        {
            return await _context.Heroes
                .FirstOrDefaultAsync(h => h.HeroName == heroName);
        }

        public async Task<bool> HeroNameExistsAsync(string heroName)
        {
            return await _context.Heroes
                .AnyAsync(h => h.HeroName == heroName);
        }

        public async Task<Hero> AddAsync(Hero hero)
        {
            try
            {
                hero.BirthDate = hero.BirthDate.ToUniversalTime();
                hero.CreatedAt = DateTime.UtcNow;

                await _context.Heroes.AddAsync(hero);
                await _context.SaveChangesAsync();

                return hero; 
            }
            catch (Exception ex)
            {
                // Log do erro (opcional)
                Console.WriteLine($"Erro ao adicionar herói: {ex.Message}");
                throw; // Re-lança a exceção para ser tratada no service
            }
        }

        public async Task UpdateAsync(Hero hero)
        {
            _context.Heroes.Update(hero);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var hero = await GetByIdAsync(id);
            if (hero != null)
            {
                _context.Heroes.Remove(hero);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Heroes.AnyAsync(e => e.Id == id);
        }


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
                    Superpowers = h.HeroSuperpowers.Select(hs => new SuperpowerDto
                    {
                        Id = hs.Superpower.Id,
                        Name = hs.Superpower.Name,
                        Description = hs.Superpower.Description
                    }).ToList()
                })
                .ToListAsync();
        }

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
                    Superpowers = h.HeroSuperpowers.Select(hs => new SuperpowerDto
                    {
                        Id = hs.Superpower.Id,
                        Name = hs.Superpower.Name,
                        Description = hs.Superpower.Description
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
    }
}
