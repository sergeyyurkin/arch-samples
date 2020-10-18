FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Persons.Api/Persons.Api.csproj", "Persons.Api/"]
RUN dotnet restore "Persons.Api/Persons.Api.csproj"
COPY . .
WORKDIR "/src/Persons.Api"
RUN dotnet build "Persons.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Persons.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Persons.Api.dll"]