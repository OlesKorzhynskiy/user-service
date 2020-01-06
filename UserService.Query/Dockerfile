FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["UserService.Query/UserService.Query.csproj", "UserService.Query/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
COPY ["UserService.Query.Services/UserService.Query.Services.csproj", "UserService.Query.Services/"]
COPY ["UserService.Query.Contracts/UserService.Query.Contracts.csproj", "UserService.Query.Contracts/"]
RUN dotnet restore "UserService.Query/UserService.Query.csproj"
COPY . .
WORKDIR "/src/UserService.Query"
RUN dotnet build "UserService.Query.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Query.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Query.dll"]