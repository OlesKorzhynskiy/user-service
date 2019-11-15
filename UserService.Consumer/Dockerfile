FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["UserService.Consumer/UserService.Consumer.csproj", "UserService.Consumer/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Contracts/UserService.Contracts.csproj", "UserService.Contracts/"]
COPY ["UserService.Mediator/UserService.Mediator.csproj", "UserService.Mediator/"]
RUN dotnet restore "UserService.Consumer/UserService.Consumer.csproj"
COPY . .
WORKDIR "/src/UserService.Consumer"
RUN dotnet build "UserService.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Consumer.dll"]