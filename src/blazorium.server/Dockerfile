#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/blazorium.server/blazorium.server.csproj", "src/blazorium.server/"]
COPY ["src/blazorium.shared/blazorium.shared.csproj", "src/blazorium.shared/"]
RUN dotnet restore "src/blazorium.server/blazorium.server.csproj"
COPY . .
WORKDIR "/src/src/blazorium.server"
RUN dotnet build "blazorium.server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "blazorium.server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "blazorium.server.dll"]