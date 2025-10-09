FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /build

# Copy Core project (dependency)
COPY ["src/Core/", "src/Core/"]

# Copy Wiremock project
COPY ["tests/Wiremock/", "tests/Wiremock/"]

# Restore dependencies
RUN dotnet restore "tests/Wiremock/Wiremock.csproj"

# Publish the application
RUN dotnet publish "tests/Wiremock/Wiremock.csproj" -c Release -o out  



FROM base AS wiremock
WORKDIR /app
COPY --from=build-env /build/out ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "Wiremock.dll"]