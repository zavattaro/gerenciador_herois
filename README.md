# Hero Manager 🦸‍♂️

Aplicação **Fullstack** para gerenciamento de super-heróis, desenvolvida como parte de um **desafio técnico trainee/júnior**.

---

## 🛠️ Tecnologias Utilizadas

- **Backend**: ASP.NET Core Web API (.NET 8) + Entity Framework Core  
- **Frontend**: Angular  
- **Banco de dados**: PostgreSQL (com Npgsql.EntityFrameworkCore.PostgreSQL)
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

---

## 📋 Pré-requisitos

Antes de executar a aplicação, certifique-se de ter instalado:
- .NET 8 SDK
- Node.js (versão 18 ou superior)
- Angular CLI
- PostgreSQL (ou Docker para container PostgreSQL)
- Git

---

🚀 Como Executar

1. Configurar o PostgreSQL:
# Usando Docker
docker run --name hero-db -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password -e POSTGRES_DB=herodb -p 5432:5432 -d postgres:latest

---

2. Backend (API .NET):
# Clone o repositório
git clone <url-do-repositorio>
cd HeroManager/Backend

# Restaure as dependências
dotnet restore

# Execute as migrações do banco de dados
dotnet ef database update

# Execute a aplicação
dotnet run

A API estará disponível em: https://localhost:7000
Swagger UI: https://localhost:7000/swagger

---

3. Frontend (Angular):
# Navegue para a pasta do frontend
cd ../Frontend

# Instale as dependências
npm install

# Execute a aplicação
ng serve

O frontend estará disponível em: http://localhost:4200

---

🗄️ Estrutura do Banco de Dados
O projeto utiliza três tabelas principais no PostgreSQL:

herois - Informações dos heróis

superpoderes - Catálogo de superpoderes

heroissuperpoderes - Relacionamento muitos-para-muitos

---

🧪 Testando a API
Use a interface Swagger ou ferramentas como Postman para testar os endpoints:

GET /api/herois - Listar todos os heróis

GET /api/herois/{id} - Buscar herói por ID

POST /api/herois - Criar novo herói

PUT /api/herois/{id} - Atualizar herói

DELETE /api/herois/{id} - Excluir herói

---

🔧 Decisões Técnicas
PostgreSQL: Escolhido por ser open-source, robusto e amplamente utilizado

Entity Framework Core: ORM para simplificar o acesso ao banco de dados

Swagger: Para documentação automática e teste da API

Angular Reactive Forms: Para validação robusta no frontend

Tratamento de erros centralizado: Middleware personalizado no backend

Docker: Para facilitar a execução do banco de dados

---

📝 Próximas Melhorias Possíveis
Autenticação e autorização

Paginação na listagem de heróis

Upload de imagens para os heróis

Logs detalhados da aplicação

Testes unitários e de integração

Docker-compose para toda a aplicação

---

🤓 Desenvolvedor
Enio Zavattaro - eniozavat@gmail.com
https://www.linkedin.com/in/eniozavattaro/

---

📄 Licença
Este projeto é desenvolvido para fins de avaliação técnica.

