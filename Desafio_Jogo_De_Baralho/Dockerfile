# Use a imagem base do .NET SDK para compilar a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie os arquivos de projeto e restaure as depend�ncias
COPY *.sln .
COPY Desafio_Jogo_De_Baralho/*.csproj ./Desafio_Jogo_De_Baralho/
COPY Desafio_Jogo_De_Baralho_Testes/*.csproj ./Desafio_Jogo_De_Baralho_Testes/
RUN dotnet restore

# Copie o restante dos arquivos e compile a aplica��o
COPY . .
WORKDIR /app/Desafio_Jogo_De_Baralho
RUN dotnet publish -c Release -o out

# Use a imagem base do .NET Runtime para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/Desafio_Jogo_De_Baralho/out .

# Exponha a porta que a aplica��o ir� rodar
EXPOSE 5000

# Comando para rodar a aplica��o
ENTRYPOINT ["dotnet", "Desafio_Jogo_De_Baralho.Api.dll"]