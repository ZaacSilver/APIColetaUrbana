# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências
COPY *.csproj .
RUN dotnet restore

# Copia o restante dos arquivos e compila a aplicação
COPY . .
RUN dotnet publish -c Release -o out

# Etapa final: cria a imagem mínima para executar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Porta exposta para acesso à aplicação
EXPOSE 80
ENTRYPOINT ["dotnet", "MyApp.dll"]
