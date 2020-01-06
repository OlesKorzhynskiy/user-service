FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["UserService.Command/UserService.Command.csproj", "UserService.Command/"]
COPY ["UserService.Command.Services/UserService.Command.Services.csproj", "UserService.Command.Services/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Command.Contracts/UserService.Command.Contracts.csproj", "UserService.Command.Contracts/"]
COPY ["UserService.Mediator/UserService.Mediator.csproj", "UserService.Mediator/"]
RUN dotnet restore "UserService.Command/UserService.Command.csproj"
COPY . .
WORKDIR "/src/UserService.Command"
RUN dotnet build "UserService.Command.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Command.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Command.dll"]