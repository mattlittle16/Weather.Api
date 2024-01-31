FROM mcr.microsoft.com/dotnet/aspnet:8.0 as base 
WORKDIR /app 
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

#copy the dlls
COPY ["Api/", "Api/"]
COPY ["Core/", "Core/"]
COPY ["Infrastructure/", "Infrastructure/"]

#restore
RUN dotnet restore "Api/Api.csproj"

COPY . . 
WORKDIR "/src/Api"

RUN dotnet build "Api.csproj" - c Release -o /app/build

#build and publish 
FROM build AS publish
RUN dotnet publish "Api/Api.csproj" -c Release -o out /app/publish 

FROM base AS final
WORKDIR /app 
COPY --from=publish /app/publish .

# Start up the application by telling runtime to run its dll
ENTRYPOINT ["dotnet", "Api.dll"]