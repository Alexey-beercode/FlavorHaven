FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlavorHaven.Presentation/FlavorHaven.Presentation.csproj", "FlavorHaven.Presentation/"]
COPY ["FlavorHaven.Infrastructure/FlavorHaven.Infrastructure.csproj", "FlavorHaven.Infrastructure/"]
COPY ["FlavorHaven.Domain/FlavorHaven.Domain.csproj", "FlavorHaven.Domain/"]
COPY ["FlavorHaven.Application/FlavorHaven.Application.csproj", "FlavorHaven.Application/"]
RUN dotnet restore "FlavorHaven.Presentation/FlavorHaven.Presentation.csproj"
COPY . .
WORKDIR "/src/FlavorHaven.Presentation"
RUN dotnet build "FlavorHaven.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FlavorHaven.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlavorHaven.Presentation.dll"]
