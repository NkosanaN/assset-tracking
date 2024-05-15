FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
    WORKDIR /app
    EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
    WORKDIR /src
    COPY ["src/API/API.csproj", "./"]
    COPY ["src/Application/Application.csproj", "./"]
    COPY ["src/Persistence/Persistence.csproj", "./"]
    COPY ["src/Infrastructure/Infrastructure.csproj", "./"]
    RUN dotnet restore "./API.csproj"
    COPY . .
    RUN dotnet build "src/API/API.csproj" -c Release -o /app

FROM build AS publish
    RUN dotnet publish "src/API/API.csproj" -c Release -o /app

FROM base AS final
    WORKDIR /app
    COPY --from=publish /app .
    ENTRYPOINT ["dotnet", "API.dll"]