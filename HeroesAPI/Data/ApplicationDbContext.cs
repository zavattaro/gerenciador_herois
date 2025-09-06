using HeroesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HeroesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Superpower> Superpowers { get; set; }
        public DbSet<HeroSuperpower> HeroSuperpowers { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeToUtcConverter>()
                .HaveColumnType("timestamp with time zone");
        }

        public class DateTimeToUtcConverter : ValueConverter<DateTime, DateTime>
        {
            public DateTimeToUtcConverter() : base(
                v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
            { }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar nomes das tabelas para snake_case
            modelBuilder.Entity<Hero>().ToTable("heroes");
            modelBuilder.Entity<Superpower>().ToTable("superpowers");
            modelBuilder.Entity<HeroSuperpower>().ToTable("hero_superpowers");

            // Configurar chave primária para HeroSuperpower
            modelBuilder.Entity<HeroSuperpower>()
                .HasKey(hs => new { hs.HeroId, hs.SuperpowerId });

            // Configurar relacionamentos
            modelBuilder.Entity<HeroSuperpower>()
                .HasOne(hs => hs.Hero)
                .WithMany(h => h.HeroSuperpowers)
                .HasForeignKey(hs => hs.HeroId);

            modelBuilder.Entity<HeroSuperpower>()
                .HasOne(hs => hs.Superpower)
                .WithMany(s => s.HeroSuperpowers)
                .HasForeignKey(hs => hs.SuperpowerId);

            // Unique constraint para HeroName
            modelBuilder.Entity<Hero>()
                .HasIndex(h => h.HeroName)
                .IsUnique();

            // Seed inicial de superpoderes
            modelBuilder.Entity<Superpower>().HasData(
                new Superpower { Id = 1, Name = "Super Força", Description = "Habilidade de levantar objetos extremamente pesados" },
                new Superpower { Id = 2, Name = "Voo", Description = "Habilidade de levitar e se mover pelo ar" },
                new Superpower { Id = 3, Name = "Super Velocidade", Description = "Habilidade de se mover em velocidades sobre-humanas" },
                new Superpower { Id = 4, Name = "Invisibilidade", Description = "Habilidade de se tornar invisível" },
                new Superpower { Id = 5, Name = "Teletransporte", Description = "Habilidade de se transportar instantaneamente" },
                new Superpower { Id = 6, Name = "Visão de Calor", Description = "Habilidade de emitir raios de calor dos olhos" },
                new Superpower { Id = 7, Name = "Regeneração", Description = "Habilidade de se curar rapidamente" },
                new Superpower { Id = 8, Name = "Elasticidade", Description = "Habilidade de esticar o corpo" },
                new Superpower { Id = 9, Name = "Telepatia", Description = "Habilidade de ler e controlar mentes" },
                new Superpower { Id = 10, Name = "Controle Elemental", Description = "Habilidade de controlar elementos naturais" },
                new Superpower { Id = 11, Name = "Sentido Aranha", Description = "Sentido elevado de perigo" },
                new Superpower { Id = 12, Name = "Disparo de Teias", Description = "Habilidade de disparar teias" },
                new Superpower { Id = 13, Name = "Escudo Indestrutível", Description = "Escudo feito de vibranium" },
                new Superpower { Id = 14, Name = "Especialista em Artes Marciais", Description = "Mestre em combate corpo a corpo" },
                new Superpower { Id = 15, Name = "Intelecto Genial", Description = "Intelecto altamente desenvolvido" },
                new Superpower { Id = 16, Name = "Manipulação da Realidade", Description = "Habilidade de alterar a realidade" },
                new Superpower { Id = 17, Name = "Magia", Description = "Habilidade de lançar feitiços" },
                new Superpower { Id = 18, Name = "Metamorfose", Description = "Habilidade de mudar de forma" },
                new Superpower { Id = 19, Name = "Super Sopro", Description = "Sopro capaz de congelar ou gerar ventos" },
                new Superpower { Id = 20, Name = "Camuflagem", Description = "Habilidade de se misturar ao ambiente" }
            );

            // Seed inicial de heróis
            var currentDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<Hero>().HasData(
                new Hero { Id = 1, Name = "Clark Kent", HeroName = "Super-Homem", BirthDate = new DateTime(1980, 2, 29), Height = 1.91, Weight = 107.5, CreatedAt = currentDate },
                new Hero { Id = 2, Name = "Bruce Wayne", HeroName = "Batman", BirthDate = new DateTime(1972, 4, 17), Height = 1.88, Weight = 95.0, CreatedAt = currentDate },
                new Hero { Id = 3, Name = "Diana Prince", HeroName = "Mulher-Maravilha", BirthDate = new DateTime(3000, 3, 22), Height = 1.83, Weight = 74.0, CreatedAt = currentDate },
                new Hero { Id = 4, Name = "Barry Allen", HeroName = "Flash", BirthDate = new DateTime(1989, 7, 14), Height = 1.80, Weight = 82.0, CreatedAt = currentDate },
                new Hero { Id = 5, Name = "Hal Jordan", HeroName = "Lanterna Verde", BirthDate = new DateTime(1985, 1, 30), Height = 1.85, Weight = 88.0, CreatedAt = currentDate },
                new Hero { Id = 6, Name = "Steve Rogers", HeroName = "Capitão América", BirthDate = new DateTime(1918, 7, 4), Height = 1.88, Weight = 108.0, CreatedAt = currentDate },
                new Hero { Id = 7, Name = "Tony Stark", HeroName = "Homem de Ferro", BirthDate = new DateTime(1970, 5, 29), Height = 1.85, Weight = 90.0, CreatedAt = currentDate },
                new Hero { Id = 8, Name = "Thor Odinson", HeroName = "Thor", BirthDate = new DateTime(965, 1, 1), Height = 1.98, Weight = 120.0, CreatedAt = currentDate },
                new Hero { Id = 9, Name = "Natasha Romanoff", HeroName = "Viúva Negra", BirthDate = new DateTime(1984, 11, 22), Height = 1.70, Weight = 57.0, CreatedAt = currentDate },
                new Hero { Id = 10, Name = "Peter Parker", HeroName = "Homem-Aranha", BirthDate = new DateTime(2001, 8, 10), Height = 1.78, Weight = 76.0, CreatedAt = currentDate }
            );

            // Seed inicial de relacionamentos herói-superpoder
            modelBuilder.Entity<HeroSuperpower>().HasData(
                // Super-Homem
                new HeroSuperpower { HeroId = 1, SuperpowerId = 1 },  // Super Força
                new HeroSuperpower { HeroId = 1, SuperpowerId = 2 },  // Voo
                new HeroSuperpower { HeroId = 1, SuperpowerId = 6 },  // Visão de Calor
                new HeroSuperpower { HeroId = 1, SuperpowerId = 19 }, // Super Sopro

                // Batman
                new HeroSuperpower { HeroId = 2, SuperpowerId = 14 }, // Especialista em Artes Marciais
                new HeroSuperpower { HeroId = 2, SuperpowerId = 15 }, // Intelecto Genial
                new HeroSuperpower { HeroId = 2, SuperpowerId = 20 }, // Camuflagem

                // Mulher-Maravilha
                new HeroSuperpower { HeroId = 3, SuperpowerId = 1 },  // Super Força
                new HeroSuperpower { HeroId = 3, SuperpowerId = 2 },  // Voo
                new HeroSuperpower { HeroId = 3, SuperpowerId = 7 },  // Regeneração

                // Flash
                new HeroSuperpower { HeroId = 4, SuperpowerId = 3 },  // Super Velocidade
                new HeroSuperpower { HeroId = 4, SuperpowerId = 5 },  // Teletransporte

                // Lanterna Verde
                new HeroSuperpower { HeroId = 5, SuperpowerId = 2 },  // Voo
                new HeroSuperpower { HeroId = 5, SuperpowerId = 16 }, // Manipulação da Realidade

                // Capitão América
                new HeroSuperpower { HeroId = 6, SuperpowerId = 1 },  // Super Força
                new HeroSuperpower { HeroId = 6, SuperpowerId = 7 },  // Regeneração
                new HeroSuperpower { HeroId = 6, SuperpowerId = 13 }, // Escudo Indestrutível

                // Homem de Ferro
                new HeroSuperpower { HeroId = 7, SuperpowerId = 2 },  // Voo
                new HeroSuperpower { HeroId = 7, SuperpowerId = 6 },  // Visão de Calor
                new HeroSuperpower { HeroId = 7, SuperpowerId = 15 }, // Intelecto Genial

                // Thor
                new HeroSuperpower { HeroId = 8, SuperpowerId = 1 },  // Super Força
                new HeroSuperpower { HeroId = 8, SuperpowerId = 2 },  // Voo
                new HeroSuperpower { HeroId = 8, SuperpowerId = 10 }, // Controle Elemental

                // Viúva Negra
                new HeroSuperpower { HeroId = 9, SuperpowerId = 14 }, // Especialista em Artes Marciais
                new HeroSuperpower { HeroId = 9, SuperpowerId = 20 }, // Camuflagem

                // Homem-Aranha
                new HeroSuperpower { HeroId = 10, SuperpowerId = 1 },  // Super Força
                new HeroSuperpower { HeroId = 10, SuperpowerId = 11 }, // Sentido Aranha
                new HeroSuperpower { HeroId = 10, SuperpowerId = 12 }  // Disparo de Teias
            );
        }
    }
}