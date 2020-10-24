FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "DbInitUseMigrator.sln" "DbInitUseMigrator.sln"

COPY "Ordering/Ordering.API/Ordering.API.csproj"                         "Ordering/Ordering.API/Ordering.API.csproj"
COPY "Ordering/Ordering.BackgroundTasks/Ordering.BackgroundTasks.csproj" "Ordering/Ordering.BackgroundTasks/Ordering.BackgroundTasks.csproj"
COPY "Ordering/Ordering.Domain/Ordering.Domain.csproj"                   "Ordering/Ordering.Domain/Ordering.Domain.csproj"
COPY "Ordering/Ordering.FunctionalTests/Ordering.FunctionalTests.csproj" "Ordering/Ordering.FunctionalTests/Ordering.FunctionalTests.csproj"
COPY "Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj"   "Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj"
COPY "Ordering/Ordering.SignalrHub/Ordering.SignalrHub.csproj"           "Ordering/Ordering.SignalrHub/Ordering.SignalrHub.csproj"
COPY "Ordering/Ordering.UnitTests/Ordering.UnitTests.csproj"             "Ordering/Ordering.UnitTests/Ordering.UnitTests.csproj"

RUN dotnet restore "DbInitUseMigrator.sln"

COPY . .
WORKDIR /src/Services/Ordering/Ordering.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build as unittest
WORKDIR /src/Services/Ordering/Ordering.UnitTests

FROM build as functionaltest
WORKDIR /src/Services/Ordering/Ordering.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]
