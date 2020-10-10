FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.12 AS build
WORKDIR /sln

COPY "./src/Identity.Api/Identity.Api.csproj" "./src/Identity.Api/"
RUN dotnet restore ./src/Identity.Api/Identity.Api.csproj

COPY "./src/Identity.Api" "./src/Identity.Api"

RUN dotnet publish "./src/Identity.Api/Identity.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine3.12
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]