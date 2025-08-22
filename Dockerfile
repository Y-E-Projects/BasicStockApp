FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY *.sln .
COPY API/*.csproj ./API/
COPY BL/*.csproj ./BL/
COPY DAL/*.csproj ./DAL/
COPY EL/*.csproj ./EL/
COPY DTO/*.csproj ./DTO/
COPY xUnitTest/*.csproj ./xUnitTest/
RUN dotnet restore

COPY . .
WORKDIR /app/API
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY https/aspnetapp.pfx /https/aspnetapp.pfx

COPY --from=build /app/API/out ./

EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=MySecretPassword

ENTRYPOINT ["dotnet", "API.dll"]
