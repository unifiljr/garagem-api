# Etapa 1 — build da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura as dependências primeiro (otimiza o cache)
COPY GaragemAPI/GaragemAPI.csproj GaragemAPI/
RUN dotnet restore GaragemAPI/GaragemAPI.csproj

# Copia o restante do código e publica
COPY GaragemAPI/. GaragemAPI/
WORKDIR /src/GaragemAPI
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 — imagem final, mais leve, só com o necessário pra rodar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# O Render define a porta via variável de ambiente PORT — configuramos o Kestrel para escutar nela
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "GaragemAPI.dll"]
