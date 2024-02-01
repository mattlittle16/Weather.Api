FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app 

COPY ["Api/", "Api/"]
COPY ["Core/", "Core/"]
COPY ["Infrastructure/", "Infrastructure/"]

RUN dotnet restore "Api/Api.csproj"

RUN dotnet publish "Api/Api.csproj" -c Release -o out 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime 
WORKDIR /app 
COPY --from=build /app/out ./ 

ENTRYPOINT ["dotnet", "Api.dll"]