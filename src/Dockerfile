#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src 
EXPOSE 80
EXPOSE 443

COPY ["ItemManagementSysterm.sln", "ItemManagementSysterm.sln"]
COPY ["API/API.csproj", "API/API.csproj"]
COPY ["Application/Application.csproj", "Application/Application.csproj"]
COPY ["Domain/Domain.csproj", "Domain/Domain.csproj"]
COPY ["Persistence/Persistence.csproj", "Persistence/Persistence.csproj"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/Infrastructure.csproj"]

RUN dotnet restore "ItemManagementSysterm.sln"

COPY . .
WORKDIR /src
RUN dotnet publish "API.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /src
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "API.dll"]