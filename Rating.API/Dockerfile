# Базовый образ для выполнения ASP.NET Core приложения
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Образ для сборки проекта
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Копируем файл решения и проекты
COPY ["RatingSystem.sln", "."]
COPY ["Rating.API/Rating.API.csproj", "Rating.API/"]
COPY ["Rating.Application/Rating.Application.csproj", "Rating.Application/"]
COPY ["Rating.DataAccess/Rating.DataAccess.csproj", "Rating.DataAccess/"]
COPY ["Rating.Domain/Rating.Domain.csproj", "Rating.Domain/"]

# Восстанавливаем зависимости для всех проектов
RUN dotnet restore "Rating.API/Rating.API.csproj"

# Копируем всё содержимое
COPY . .

# Сборка проекта
WORKDIR "/src/Rating.API"
RUN dotnet build "Rating.API.csproj" -c Release -o /app/build

# Публикация проекта
FROM build AS publish
RUN dotnet publish "Rating.API.csproj" -c Release -o /app/publish

# Финальный этап — создание контейнера для запуска
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rating.API.dll"]
