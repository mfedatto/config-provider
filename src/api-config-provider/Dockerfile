FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fedatto.ConfigProvider.WebApi/Fedatto.ConfigProvider.WebApi.csproj", "Fedatto.ConfigProvider.WebApi/"]
COPY ["Fedatto.ConfigProvider.CrossCutting/Fedatto.ConfigProvider.CrossCutting.csproj", "Fedatto.ConfigProvider.CrossCutting/"]
COPY ["Fedatto.ConfigProvider.Application/Fedatto.ConfigProvider.Application.csproj", "Fedatto.ConfigProvider.Application/"]
COPY ["Fedatto.ConfigProvider.Domain/Fedatto.ConfigProvider.Domain.csproj", "Fedatto.ConfigProvider.Domain/"]
COPY ["Fedatto.HttpExceptions/Fedatto.HttpExceptions.csproj", "Fedatto.HttpExceptions/"]
COPY ["Fedatto.ConfigProvider.Infrastructure/Fedatto.ConfigProvider.Infrastructure.csproj", "Fedatto.ConfigProvider.Infrastructure/"]
COPY ["Fedatto.ConfigProvider.Service/Fedatto.ConfigProvider.Service.csproj", "Fedatto.ConfigProvider.Service/"]
RUN dotnet restore "Fedatto.ConfigProvider.WebApi/Fedatto.ConfigProvider.WebApi.csproj"
COPY . .
WORKDIR "/src/Fedatto.ConfigProvider.WebApi"
RUN dotnet build "Fedatto.ConfigProvider.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Fedatto.ConfigProvider.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fedatto.ConfigProvider.WebApi.dll"]
