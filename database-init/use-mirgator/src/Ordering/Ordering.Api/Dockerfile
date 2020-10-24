FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "DbInitUseMigrator.sln" "DbInitUseMigrator.sln"

COPY "Ordering/Ordering.Api/Ordering.Api.csproj"            "Ordering/Ordering.Api/Ordering.Api.csproj"
COPY "Ordering/Ordering.Core/Ordering.Core.csproj"          "Ordering/Ordering.Core/Ordering.Core.csproj"
COPY "Ordering/Ordering.EFCore/Ordering.EFCore.csproj"      "Ordering/Ordering.EFCore/Ordering.EFCore.csproj"
COPY "Ordering/Ordering.Migrator/Ordering.Migrator.csproj"  "Ordering/Ordering.Migrator/Ordering.Migrator.csproj"

RUN dotnet restore "DbInitUseMigrator.sln"

COPY . .
WORKDIR "/src/Ordering/Ordering.Api"
RUN dotnet publish --no-restore -c Release -o /app

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]