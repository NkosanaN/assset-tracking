#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY ["ItemManagementSysterm.sln", "ItemManagementSysterm.sln"]
COPY ["src/API/API.csproj", "src/API/API.csproj"]
#COPY ["test/T1/ItemManagementSystem.T1.Tests/ItemManagementSystem.T1.Tests.csproj", "test/T1/ItemManagementSystem.T1.Tests/"]
COPY ["src/Application/Application.csproj", "src/Application/Application.csproj"]
COPY ["src/Domain/Domain.csproj", "src/Domain/Domain.csproj"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/Persistence.csproj"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/Infrastructure.csproj"]

RUN dotnet restore "src/API/API.csproj"

COPY . .
WORKDIR /app
RUN dotnet publish "API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]