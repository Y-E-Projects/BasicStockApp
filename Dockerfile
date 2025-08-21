# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# csproj dosyalarını kopyala ve restore et
COPY *.sln .
COPY API/*.csproj ./API/
COPY BL/*.csproj ./BL/
COPY DAL/*.csproj ./DAL/
COPY EL/*.csproj ./EL/
COPY DTO/*.csproj ./DTO/
COPY xUnitTest/*.csproj ./xUnitTest/
RUN dotnet restore

# Proje dosyalarını kopyala ve publish et
COPY . .
WORKDIR /app/API
RUN dotnet publish -c Release -o out

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# PFX sertifikasını image içine ekle
COPY https/aspnetapp.pfx /https/aspnetapp.pfx

# Publish edilmiş API dosyalarını kopyala
COPY --from=build /app/API/out ./

# HTTP ve HTTPS portlarını aç
EXPOSE 5000
EXPOSE 5001

# Sertifika ayarları (Program.cs bunları otomatik okur)
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=MySecretPassword

ENTRYPOINT ["dotnet", "API.dll"]
