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

## 🚀 Como Executar

### 1. Configurar o PostgreSQL
```bash
# Usando Docker
docker run --name hero-db -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=password -e POSTGRES_DB=herodb -p 5432:5432 -d postgres:latest
