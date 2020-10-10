FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.12 AS build
WORKDIR /sln

COPY "./src/Persons.Api/Persons.Api.csproj" "./src/Persons.Api/"
RUN dotnet restore ./src/Persons.Api/Persons.Api.csproj

COPY "./src/Persons.Api" "./src/Persons.Api"

RUN dotnet publish "./src/Persons.Api/Persons.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.12
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Persons.Api.dll"]