
# Restore de dependências .NET (cache layer)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS restore

WORKDIR /src

# Copia apenas os .csproj para aproveitar o cache do Docker
COPY MeCompraUmCafe.sln ./
COPY API/*.csproj             ./API/
COPY Application/*.csproj     ./Application/
COPY Domain/*.csproj          ./Domain/
COPY Infrastructure/*.csproj  ./Infrastructure/

RUN dotnet restore API/API.csproj

# Build e Publish da API
FROM restore AS build

# Copia o restante do código fonte
COPY API/             ./API/
COPY Application/     ./Application/
COPY Domain/          ./Domain/
COPY Infrastructure/  ./Infrastructure/

ENV NUGET_FALLBACK_PACKAGES=""

RUN dotnet publish API/API.csproj \
    --configuration Release \
    #--no-restore \
    --output /app/publish

# Imagem final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final

WORKDIR /app

# Copia os binários publicados da API
COPY --from=build /app/publish .

# Porta padrão do ASP.NET
EXPOSE 8080

ENTRYPOINT ["dotnet", "API.dll"]
