FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /build

RUN dir -s    

COPY ["./src/Core/", "Core/"]
COPY ["./src/Wiremock/", "Wiremock/"]

RUN dotnet restore "Wiremock/Wiremock.csproj"

RUN dotnet publish "Wiremock/Wiremock.csproj" -c Debug -o out  



FROM base as Wiremock
WORKDIR /app
COPY --from=build-env /build/out ./

EXPOSE 8080

ENTRYPOINT ["dotnet", "Wiremock.dll"]