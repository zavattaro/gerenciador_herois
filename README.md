# Hero Manager 🦸‍♂️

Aplicação **Fullstack** para gerenciamento de super-heróis, desenvolvida como parte de um **desafio técnico trainee/júnior**.

---

## 🛠️ Tecnologias Utilizadas

- **Backend**: ASP.NET Core Web API (.NET 8) + Entity Framework Core  
- **Frontend**: Angular  
- **Banco de dados**: PostgreSQL 15 (Docker Container)
- **Containerização**: Docker + Docker Compose
- **Documentação**: Swagger/OpenAPI

---

## 🚀 Funcionalidades

- ✅ Cadastro de novos heróis  
- ✅ Listagem de todos os heróis cadastrados  
- ✅ Consulta de herói por **Id**  
- ✅ Atualização dos dados de um herói  
- ✅ Exclusão de um herói  
- ✅ Documentação da API com **Swagger**  
- ✅ Validação de nome de herói único
- ✅ Associação de múltiplos superpoderes
- ✅ Tratamento de erros e respostas HTTP apropriadas
- ✅ **Deploy automatizado** com Docker Compose

---

## 📋 Pré-requisitos

Antes de executar a aplicação, certifique-se de ter instalado:
- **Docker Desktop** (Windows/Mac) ou **Docker Engine** (Linux)
- **Docker Compose** (já incluído no Docker Desktop)
- Git

---

## 🐳 Como Executar com Docker Compose
```bash
### 1. **Clone o repositório**
(abra o bash em uma pasta vazia)

git clone https://github.com/zavattaro/gerenciador_herois.git

2. Navegue para a pasta clonada
cd gerenciador_herois

3. Execute a aplicação com um comando
docker-compose up -d --build
```

⚠️ Nota Importante:
A primeira execução pode demorar alguns minutos para baixar as imagens

## 📖 Documentação da API

 - A documentação Swagger é gerada automaticamente a partir dos **XML Comments** (`/// <summary>`) nos controllers. Optamos por esta abordagem em vez de anotações Swagger tradicionais (`[SwaggerOperation]`, `[SwaggerResponse]`) **para resolver problemas de build no Docker**, onde as dependências do Swagger causavam erros durante o processo de publish do container.
 
- ✅ **Estabilidade**: Evita conflitos de pacotes no Docker build
- ✅ **Manutenibilidade**: Documentação junto ao código-fonte
- ✅ **Performance**: Menos dependências externas  
- ✅ **Compatibilidade**: Funciona em qualquer ambiente .NET


# Acesse as aplicações
API Backend: http://localhost:8080

Swagger UI: http://localhost:8080/swagger

Frontend Angular: http://localhost:4200 (se já configurado)

PostgreSQL: localhost:5432

--- 

## 🏗️ Arquitetura e Organização

### Estrutura de Pastas

**HeroesAPI/**
- **📁 Controllers/** - Endpoints da API (Minimal API)
- **📁 Models/** - Entidades de Domínio (Hero, Superpower)  
- **📁 DTO/** - Data Transfer Objects
- **📁 Data/** - Entity Framework Context
- **📁 Repositories/** - Padrão Repository
- **📁 Service/** - Camada de Serviços
- **📁 Properties/** - Configurações de execução

### Decisões Arquiteturais

**1. Clean Architecture Simplificada**
- Separação clara entre responsabilidades
- Controllers → Services → Repositories → Data
- Injeção de dependência via construtor

**2. Padrão Repository**
- Interfaces em `Repositories/Interfaces/`
- Implementações concretas em `Repositories/`
- Abstração completa do acesso a dados

**3. Service Layer**
- Lógica de negócio isolada em `Service/`
- Validações complexas centralizadas
- `IHeroService` e `ISuperpowerService` com contratos claros

**4. Entity Framework Code-First**
- `ApplicationDbContext` configurado para PostgreSQL
- Migrations para controle de versão do schema
- Tabela `HeroSuperpower` para relação N-N

**5. DTO Pattern**
- `CreateHeroRequestDto`: Validações de input
- `HeroWithSuperpowersDto`: Response com relacionamentos
- Prevenção de over-posting e exposure

### Tecnologias e Padrões
- **.NET 8**: Performance e recursos modernos
- **Entity Framework Core**: ORM com migrations
- **PostgreSQL**: Banco relacional robusto
- **Repository Pattern**: Abstração de persistência
- **Dependency Injection**: Acoplamento mínimo
- **Swagger**: Documentação automática da API

### Vantagens desta Estrutura
- ✅ **Testabilidade**: Camadas isoladas facilitam testes
- ✅ **Manutenibilidade**: Código organizado e claro
- ✅ **Escalabilidade**: Fácil adição de novas features
- ✅ **Flexibilidade**: Troca de implementações simplificada


## 🏗️ Arquitetura Docker
A aplicação utiliza 3 containers interconectados:

yaml
services:
  hero-db:     # PostgreSQL 15 com dados pré-populados
  hero-api:    # API .NET 8 com Auto-Migration
  hero-sdk:    # .NET SDK para desenvolvimento

📦 Serviços Docker
Container	Descrição	Porta
hero-db	Banco PostgreSQL com dados iniciais	5432
hero-api	API .NET com Swagger	8080
hero-sdk	Ambiente de desenvolvimento	-

🗄️ Estrutura do Banco de Dados
O projeto utiliza três tabelas principais no PostgreSQL:

heroes - Informações dos heróis

superpowers - Catálogo de superpoderes

hero_superpowers - Relacionamento muitos-para-muitos

📊 Dados Iniciais
O sistema já vem populado com:

🦸 10 heróis famosos (Super-Homem, Batman, etc.)

⚡ 20 superpoderes diferentes

🔗 Relacionamentos pré-definidos

## 🧪 Testando a API

### Via Swagger UI
Acesse: http://localhost:8080/swagger

### Via HTTP Requests
```bash
# Listar heróis
GET http://localhost:8080/api/Hero

# Buscar herói por ID  
GET http://localhost:8080/api/Hero/1

# Criar novo herói
curl -X POST "http://localhost:8080/api/Hero" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Wanda Maximoff",
    "heroName": "Feiticeira Escarlate", 
    "birthDate": "1989-02-10",
    "height": 1.70,
    "weight": 59.0,
    "superpowerIds": [9, 17]
  }'

🔧 Funcionalidades Técnicas Avançadas
🐳 Containerização Completa: Todos os serviços em Docker

🔄 Auto-Migration: Banco criado automaticamente no startup

📦 Data Seeding: Dados iniciais populados automaticamente

🔒 Health Checks: Monitoramento de saúde dos containers

🌐 Network Isolation: Rede privada entre containers

📊 Volume Persistence: Dados persistidos em volume Docker

🚀 Comandos Úteis
bash
# Ver status dos containers
docker-compose ps

# Ver logs da API
docker-compose logs api

# Parar aplicação
docker-compose down

# Parar e remover volumes
docker-compose down -v

# Rebuildar imagens
docker-compose up -d --build

📝 Próximas Melhorias Possíveis
Autenticação e autorização (JWT)

Paginação na listagem de heróis

Upload de imagens para os heróis

Logs centralizados (ELK Stack)

Testes unitários e de integração

Monitoramento (Prometheus + Grafana)

CI/CD pipeline (GitHub Actions)

🤓 Desenvolvedor
Enio Zavattaro - eniozavat@gmail.com
https://www.linkedin.com/in/eniozavattaro/

📄 Licença
Este projeto é desenvolvido para fins de avaliação técnica.

🙋‍♂️ Suporte
Em caso de problemas com a execução:

Verifique se o Docker está rodando

Execute docker-compose down -v e suba novamente

Consulte os logs com docker-compose logs api
