FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#COPY ["ItemManagementSysterm.sln", "ItemManagementSysterm.sln"]
COPY ["src/API/API.csproj", "src/API/API.csproj"]
COPY ["src/Application/Application.csproj", "src/Application/Application.csproj"]
COPY ["src/Domain/Domain.csproj", "src/Domain/Domain.csproj"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/Persistence.csproj"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/Infrastructure.csproj"]

RUN dotnet restore "src/API/API.csproj"

COPY . .
WORKDIR /app
RUN dotnet publish "src/API.csproj" -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=base /app/out .
ENTRYPOINT ["dotnet", "API.dll"]