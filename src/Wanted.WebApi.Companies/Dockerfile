FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["Wanted.WebApi.Companies/Wanted.WebApi.Companies.csproj", "Wanted.WebApi.Companies/"]
RUN dotnet restore "Wanted.WebApi.Companies/Wanted.WebApi.Companies.csproj"
COPY . .
WORKDIR "/src/Wanted.WebApi.Companies"
RUN dotnet build "Wanted.WebApi.Companies.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Wanted.WebApi.Companies.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wanted.WebApi.Companies.dll"]
