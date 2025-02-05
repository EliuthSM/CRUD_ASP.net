# Imagen base para el runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Imagen para la compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia y restaura las dependencias primero
COPY ["CRUD_ASP.net/CRUD_ASP.net.csproj", "CRUD_ASP.net/"]
RUN dotnet restore "CRUD_ASP.net/CRUD_ASP.net.csproj"

# Copia el resto del código y compila
COPY . .
RUN dotnet build "CRUD_ASP.net/CRUD_ASP.net.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "CRUD_ASP.net/CRUD_ASP.net.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUD_ASP.net.dll"]