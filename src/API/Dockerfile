WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN dotnet restore "src/API/API.csproj"
COPY . .
WORKDIR "/src/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build


WORKDIR /app
ENTRYPOINT ["dotnet", "API.dll"]