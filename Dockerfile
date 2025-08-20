# 1. Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# .csproj dosyalarýný kopyala ve restore et
COPY *.sln .
COPY API/*.csproj ./API/
COPY BL/*.csproj ./BL/
COPY DAL/*.csproj ./DAL/
COPY EL/*.csproj ./EL/
COPY DTO/*.csproj ./DTO/
COPY xUnitTest/*.csproj ./xUnitTest/
RUN dotnet restore

# Tüm projeyi kopyala ve publish et
COPY . .
WORKDIR /app/API
RUN dotnet publish -c Release -o out

# 2. Runtime aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "API.dll"]
