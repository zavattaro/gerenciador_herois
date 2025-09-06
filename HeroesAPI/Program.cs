using HeroesAPI.Data;
using HeroesAPI.Repositories.Interfaces;
using HeroesAPI.Repositories;
using HeroesAPI.Services;
using HeroesAPI.Models;
using Microsoft.EntityFrameworkCore;
using HeroesAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Adiciona CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IHeroRepository, HeroRepository>();
builder.Services.AddScoped<IHeroService, HeroService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// =============================================
// CRIAÇÃO AUTOMÁTICA DO BANCO E SEED
// =============================================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // 1. Criar tabelas automaticamente (sem migrations)
    Console.WriteLine("🏗️ Criando tabelas do banco de dados...");
    try
    {
        dbContext.Database.EnsureCreated();
        Console.WriteLine("✅ Tabelas criadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao criar tabelas: {ex.Message}");
        throw;
    }

    // 2. Pequena pausa para garantir que as tabelas foram criadas
    await Task.Delay(1000);

    // 3. Verificação e população de dados
    try
    {
        // Verifica se a tabela heroes existe e está vazia
        var existingHeroes = dbContext.Heroes.Count();

        if (existingHeroes == 0)
        {
            Console.WriteLine("📦 Populando dados iniciais...");

            // POPULAÇÃO MANUAL DE SUPERPODERES
            var superpowers = new List<Superpower>
            {
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
            };

            // POPULAÇÃO MANUAL DE HERÓIS com data estática
            var staticDate = new DateTime(2025, 9, 6, 13, 22, 0, DateTimeKind.Utc);
            var heroes = new List<Hero>
            {
                new Hero { Id = 1, Name = "Clark Kent", HeroName = "Super-Homem", BirthDate = new DateTime(1980, 2, 29), Height = 1.91f, Weight = 107.5f, CreatedAt = staticDate },
                new Hero { Id = 2, Name = "Bruce Wayne", HeroName = "Batman", BirthDate = new DateTime(1972, 4, 17), Height = 1.88f, Weight = 95.0f, CreatedAt = staticDate },
                new Hero { Id = 3, Name = "Diana Prince", HeroName = "Mulher-Maravilha", BirthDate = new DateTime(3000, 3, 22), Height = 1.83f, Weight = 74.0f, CreatedAt = staticDate },
                new Hero { Id = 4, Name = "Barry Allen", HeroName = "Flash", BirthDate = new DateTime(1989, 7, 14), Height = 1.80f, Weight = 82.0f, CreatedAt = staticDate },
                new Hero { Id = 5, Name = "Hal Jordan", HeroName = "Lanterna Verde", BirthDate = new DateTime(1985, 1, 30), Height = 1.85f, Weight = 88.0f, CreatedAt = staticDate },
                new Hero { Id = 6, Name = "Steve Rogers", HeroName = "Capitão América", BirthDate = new DateTime(1918, 7, 4), Height = 1.88f, Weight = 108.0f, CreatedAt = staticDate },
                new Hero { Id = 7, Name = "Tony Stark", HeroName = "Homem de Ferro", BirthDate = new DateTime(1970, 5, 29), Height = 1.85f, Weight = 90.0f, CreatedAt = staticDate },
                new Hero { Id = 8, Name = "Thor Odinson", HeroName = "Thor", BirthDate = new DateTime(965, 1, 1), Height = 1.98f, Weight = 120.0f, CreatedAt = staticDate },
                new Hero { Id = 9, Name = "Natasha Romanoff", HeroName = "Viúva Negra", BirthDate = new DateTime(1984, 11, 22), Height = 1.70f, Weight = 57.0f, CreatedAt = staticDate },
                new Hero { Id = 10, Name = "Peter Parker", HeroName = "Homem-Aranha", BirthDate = new DateTime(2001, 8, 10), Height = 1.78f, Weight = 76.0f, CreatedAt = staticDate }
            };

            // POPULAÇÃO MANUAL DE RELACIONAMENTOS
            var heroSuperpowers = new List<HeroSuperpower>
            {
                new HeroSuperpower { HeroId = 1, SuperpowerId = 1 },
                new HeroSuperpower { HeroId = 1, SuperpowerId = 2 },
                new HeroSuperpower { HeroId = 1, SuperpowerId = 6 },
                new HeroSuperpower { HeroId = 1, SuperpowerId = 19 },

                new HeroSuperpower { HeroId = 2, SuperpowerId = 14 },
                new HeroSuperpower { HeroId = 2, SuperpowerId = 15 },
                new HeroSuperpower { HeroId = 2, SuperpowerId = 20 },

                new HeroSuperpower { HeroId = 3, SuperpowerId = 1 },
                new HeroSuperpower { HeroId = 3, SuperpowerId = 2 },
                new HeroSuperpower { HeroId = 3, SuperpowerId = 7 },

                new HeroSuperpower { HeroId = 4, SuperpowerId = 3 },
                new HeroSuperpower { HeroId = 4, SuperpowerId = 5 },

                new HeroSuperpower { HeroId = 5, SuperpowerId = 2 },
                new HeroSuperpower { HeroId = 5, SuperpowerId = 16 },

                new HeroSuperpower { HeroId = 6, SuperpowerId = 1 },
                new HeroSuperpower { HeroId = 6, SuperpowerId = 7 },
                new HeroSuperpower { HeroId = 6, SuperpowerId = 13 },

                new HeroSuperpower { HeroId = 7, SuperpowerId = 2 },
                new HeroSuperpower { HeroId = 7, SuperpowerId = 6 },
                new HeroSuperpower { HeroId = 7, SuperpowerId = 15 },

                new HeroSuperpower { HeroId = 8, SuperpowerId = 1 },
                new HeroSuperpower { HeroId = 8, SuperpowerId = 2 },
                new HeroSuperpower { HeroId = 8, SuperpowerId = 10 },

                new HeroSuperpower { HeroId = 9, SuperpowerId = 14 },
                new HeroSuperpower { HeroId = 9, SuperpowerId = 20 },

                new HeroSuperpower { HeroId = 10, SuperpowerId = 1 },
                new HeroSuperpower { HeroId = 10, SuperpowerId = 11 },
                new HeroSuperpower { HeroId = 10, SuperpowerId = 12 }
            };

            // SALVAR NO BANCO
            await dbContext.Superpowers.AddRangeAsync(superpowers);
            await dbContext.Heroes.AddRangeAsync(heroes);
            await dbContext.SaveChangesAsync();

            await dbContext.HeroSuperpowers.AddRangeAsync(heroSuperpowers);
            await dbContext.SaveChangesAsync();

            Console.WriteLine("✅ Dados iniciais populados com sucesso!");
        }
        else
        {
            Console.WriteLine($"✅ Banco já contém {existingHeroes} heróis");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Erro ao verificar/popular dados: {ex.Message}");
        // Não quebra a aplicação, só registra o erro
    }
}

app.Run();