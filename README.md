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

### 1. **Clone o repositório**
```bash
git clone https://github.com/zavattaro/gerenciador_herois.git
cd HeroManager
2. Execute a aplicação com um comando
bash
docker-compose up -d
3. Acesse as aplicações
API Backend: http://localhost:8080

Swagger UI: http://localhost:8080/swagger

Frontend Angular: http://localhost:4200 (se configurado)

PostgreSQL: localhost:5432

🏗️ Arquitetura Docker
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

🧪 Testando a API
Via Swagger UI
Acesse: http://localhost:8080/swagger

Via curl
bash
# Listar todos os heróis
curl http://localhost:8080/api/heroes

# Criar novo herói
curl -X POST "http://localhost:8080/api/heroes" \
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
