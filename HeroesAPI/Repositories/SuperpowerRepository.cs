using HeroesAPI.Data;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HeroesAPI.Repositories
{
    public class SuperpowerRepository : ISuperpowerRepository
    {
        private readonly ApplicationDbContext _context;

        public SuperpowerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Superpower>> GetAllAsync()
        {
            return await _context.Superpowers
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<Superpower?> GetByIdAsync(int id)
        {
            return await _context.Superpowers.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Superpowers.AnyAsync(s => s.Id == id);
        }

        public async Task<Superpower> AddAsync(Superpower superpower)
        {
            await _context.Superpowers.AddAsync(superpower);
            await _context.SaveChangesAsync();
            return superpower;
        }

        public async Task UpdateAsync(Superpower superpower)
        {
            _context.Superpowers.Update(superpower);
            await _context.SaveChangesAsync();
        }

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