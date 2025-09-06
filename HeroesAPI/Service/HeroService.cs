using HeroesAPI.Data;
using HeroesAPI.DTOs;
using HeroesAPI.Models;
using HeroesAPI.Repositories;
using HeroesAPI.Repositories.Interfaces;
using HeroesAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesAPI.Service
{
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroRepository;

        public HeroService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<IEnumerable<HeroWithSuperpowersDto>> GetAllHeroesAsync()
        {
            return await _heroRepository.GetHeroesWithSuperpowersAsync();
        }

        public async Task<HeroWithSuperpowersDto?> GetHeroByIdAsync(int id)
        {
            return await _heroRepository.GetHeroWithSuperpowersByIdAsync(id);
        }

        public async Task<Hero> CreateHeroAsync(Hero hero)
        {
            // Validar se nome de herói já existe
            if (await _heroRepository.HeroNameExistsAsync(hero.HeroName))
            {
                throw new System.Exception($"Herói com o nome {hero.HeroName} já existe");
            }

            await _heroRepository.AddAsync(hero);
            return hero;
        }

        public async Task UpdateHeroAsync(int id, Hero hero)
        {
            if (id != hero.Id)
            {
                throw new System.Exception("ID mismatch");
            }

            // Busca o herói existente no banco de dados
            var existingHero = await _heroRepository.GetByIdAsync(id);
            if (existingHero == null)
            {
                throw new System.Exception("Hero not found");
            }

            // Validar se nome de herói já existe
            var heroWithSameName = await _heroRepository.GetByHeroNameAsync(hero.HeroName);
            if (heroWithSameName != null && heroWithSameName.Id != id)
            {
                throw new System.Exception($"Hero with name {hero.HeroName} already exists");
            }

            // Atualizar apenas os campos necessários (evita problemas de tracking)
            existingHero.Name = hero.Name;
            existingHero.HeroName = hero.HeroName;
            existingHero.BirthDate = hero.BirthDate;
            existingHero.Height = hero.Height;
            existingHero.Weight = hero.Weight;

            await _heroRepository.UpdateAsync(existingHero);
        }

        public async Task DeleteHeroAsync(int id)
        {
            await _heroRepository.DeleteAsync(id);
        }

        public async Task<bool> HeroExistsAsync(int id)
        {
            return await _heroRepository.ExistsAsync(id);
        }
    }
}