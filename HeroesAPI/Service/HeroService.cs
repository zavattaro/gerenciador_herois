using HeroesAPI.Dtos;
using HeroesAPI.DTOs;
using HeroesAPI.Models;
using HeroesAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeroesAPI.Services
{
    /// <summary>
    /// Serviço para gerenciamento de heróis
    /// </summary>
    public class HeroService : IHeroService
    {
        private readonly IHeroRepository _heroRepository;

        /// <summary>
        /// Construtor do serviço de heróis
        /// </summary>
        /// <param name="heroRepository">Repositório de heróis</param>
        public HeroService(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        /// <summary>
        /// Obtém todos os heróis com seus superpoderes
        /// </summary>
        /// <returns>Lista de heróis com superpoderes</returns>
        public async Task<IEnumerable<HeroWithSuperpowersDto>> GetAllHeroesAsync()
        {
            return await _heroRepository.GetHeroesWithSuperpowersAsync();
        }

        /// <summary>
        /// Obtém um herói específico pelo ID
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>Herói encontrado ou null se não existir</returns>
        public async Task<HeroWithSuperpowersDto?> GetHeroByIdAsync(int id)
        {
            return await _heroRepository.GetHeroWithSuperpowersByIdAsync(id);
        }

        /// <summary>
        /// Cria um novo herói
        /// </summary>
        /// <param name="createHeroRequest">Dados do herói a ser criado</param>
        /// <returns>Herói criado com superpoderes</returns>
        /// <exception cref="InvalidOperationException">Lançada quando nome já existe ou superpoder não encontrado</exception>
        public async Task<HeroWithSuperpowersDto> CreateHeroAsync(CreateHeroRequestDto createHeroRequest)
        {
            // 1. Validar se nome já existe
            if (await _heroRepository.HeroNameExistsAsync(createHeroRequest.HeroName))
            {
                throw new InvalidOperationException($"Herói com o nome {createHeroRequest.HeroName} já existe");
            }

            // 2. Validar se todos os superpoderes existem
            foreach (var superpowerId in createHeroRequest.SuperpowerIds)
            {
                if (!await _heroRepository.SuperpowerExistsAsync(superpowerId))
                {
                    throw new InvalidOperationException($"Superpoder com ID {superpowerId} não encontrado");
                }
            }

            // 3. Converter DTO para Model
            var hero = new Hero
            {
                Name = createHeroRequest.Name,
                HeroName = createHeroRequest.HeroName,
                BirthDate = createHeroRequest.BirthDate,
                Height = createHeroRequest.Height,
                Weight = createHeroRequest.Weight
            };

            // 4. Salvar herói no bd
            var createdHero = await _heroRepository.AddAsync(hero, createHeroRequest.SuperpowerIds);

            // 5. Buscar e retornar o herói com superpoderes
            return await _heroRepository.GetHeroWithSuperpowersByIdAsync(createdHero.Id)
                ?? throw new InvalidOperationException("Erro ao criar herói");
        }

        /// <summary>
        /// Atualiza um herói existente
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <param name="updateHeroRequest">Dados atualizados do herói</param>
        /// <exception cref="ArgumentException">Lançada quando há mismatch de ID</exception>
        /// <exception cref="KeyNotFoundException">Lançada quando herói não existe</exception>
        /// <exception cref="InvalidOperationException">Lançada quando nome já existe ou superpoder não encontrado</exception>
        public async Task UpdateHeroAsync(int id, UpdateHeroRequestDto updateHeroRequest)
        {
            if (id != updateHeroRequest.Id)
            {
                throw new ArgumentException("ID mismatch");
            }

            // Busca o herói existente
            var existingHero = await _heroRepository.GetByIdAsync(id);
            if (existingHero == null)
            {
                throw new KeyNotFoundException("Herói não encontrado");
            }

            // Validar se nome de herói já existe (para outro herói)
            var heroWithSameName = await _heroRepository.GetByHeroNameAsync(updateHeroRequest.HeroName);
            if (heroWithSameName != null && heroWithSameName.Id != id)
            {
                throw new InvalidOperationException($"Herói com o nome {updateHeroRequest.HeroName} já existe");
            }

            // Validar se todos os superpoderes existem
            foreach (var superpowerId in updateHeroRequest.SuperpowerIds)
            {
                if (!await _heroRepository.SuperpowerExistsAsync(superpowerId))
                {
                    throw new InvalidOperationException($"Superpoder com ID {superpowerId} não encontrado");
                }
            }

            // Atualizar propriedades
            existingHero.Name = updateHeroRequest.Name;
            existingHero.HeroName = updateHeroRequest.HeroName;
            existingHero.BirthDate = updateHeroRequest.BirthDate;
            existingHero.Height = updateHeroRequest.Height;
            existingHero.Weight = updateHeroRequest.Weight;

            await _heroRepository.UpdateAsync(existingHero, updateHeroRequest.SuperpowerIds);
        }

        /// <summary>
        /// Exclui um herói
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <exception cref="KeyNotFoundException">Lançada quando herói não existe</exception>
        public async Task DeleteHeroAsync(int id)
        {
            if (!await _heroRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException("Herói não encontrado");
            }

            await _heroRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Verifica se um herói existe
        /// </summary>
        /// <param name="id">ID do herói</param>
        /// <returns>True se o herói existe, False caso contrário</returns>
        public async Task<bool> HeroExistsAsync(int id)
        {
            return await _heroRepository.ExistsAsync(id);
        }
    }
}