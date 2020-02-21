FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["UserService.Query.Grpc/UserService.Query.Grpc.csproj", "UserService.Query.Grpc/"]
COPY ["UserService.Infrastructure/UserService.Infrastructure.csproj", "UserService.Infrastructure/"]
COPY ["UserService.Domain/UserService.Domain.csproj", "UserService.Domain/"]
RUN dotnet restore "UserService.Query.Grpc/UserService.Query.Grpc.csproj"
COPY . .
WORKDIR "/src/UserService.Query.Grpc"
RUN dotnet build "UserService.Query.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Query.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Query.Grpc.dll"]