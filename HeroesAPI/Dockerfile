# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar apenas o .csproj para habilitar cache no restore
COPY HeroesAPI.csproj ./

# Restaurar pacotes
RUN dotnet restore

# Copiar todo o restante do código
COPY . ./

# Publicar a aplicação em Release
RUN dotnet publish -c Release -o /app /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copiar os artefatos do build
COPY --from=build /app ./

# Expor porta da aplicação
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Executar a aplicação
ENTRYPOINT ["dotnet", "HeroesAPI.dll"]
