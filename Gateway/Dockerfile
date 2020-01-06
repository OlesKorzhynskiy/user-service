FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Gateway/Gateway.csproj", "Gateway/"]
COPY ["Gateway.Contracts/Gateway.Contracts.csproj", "Gateway.Contracts/"]
COPY ["Gateway.UserService.Adapter/Gateway.UserService.Adapter.csproj", "Gateway.UserService.Adapter/"]
COPY ["UserService.Query.Client/UserService.Query.Client.csproj", "UserService.Query.Client/"]
COPY ["UserService.Query.Contracts/UserService.Query.Contracts.csproj", "UserService.Query.Contracts/"]
COPY ["UserService.Command.Contracts/UserService.Command.Contracts.csproj", "UserService.Command.Contracts/"]
COPY ["UserService.Mediator/UserService.Mediator.csproj", "UserService.Mediator/"]
RUN dotnet restore "Gateway/Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway"
RUN dotnet build "Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gateway.dll"]