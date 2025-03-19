# Use a imagem base do .NET SDK para compilar e executar os testes
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos de projeto e restaure as dependências
COPY *.sln .
COPY Desafio_Jogo_De_Baralho/*.csproj ./Desafio_Jogo_De_Baralho/
COPY Desafio_Jogo_De_Baralho_Testes/*.csproj ./Desafio_Jogo_De_Baralho_Testes/
RUN dotnet restore

# Copie o restante dos arquivos e compile a aplicação e os testes
COPY Desafio_Jogo_De_Baralho ./Desafio_Jogo_De_Baralho
COPY Desafio_Jogo_De_Baralho_Testes ./Desafio_Jogo_De_Baralho_Testes
WORKDIR /app/Desafio_Jogo_De_Baralho_Testes
RUN dotnet build

# Comando para executar os testes
CMD ["dotnet", "test"]