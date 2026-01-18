# --------------------------------------------------------------------------------
# Stage 1: Build - Optimize for Docker Caching (Dependency Restore Layer)
# --------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY src/API/API.csproj ./src/API/
COPY src/Application/Application.csproj ./src/Application/
COPY src/Models/Models.csproj ./src/Models/
COPY src/Infrastructure/Infrastructure.csproj ./src/Infrastructure/

RUN dotnet restore ./src/API/API.csproj

COPY src/API/ ./src/API/
COPY src/Application/ ./src/Application/
COPY src/Models/ ./src/Models/
COPY src/Infrastructure/ ./src/Infrastructure/

RUN dotnet publish ./src/API/API.csproj -c Release -o /app/publish

# --------------------------------------------------------------------------------
# Stage 2: Runtime
# --------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "API.dll"]
