#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/blazorium.producer/blazorium.producer.csproj", "src/blazorium.producer/"]
RUN dotnet restore "src/blazorium.producer/blazorium.producer.csproj"
COPY . .
WORKDIR "/src/src/blazorium.producer"
RUN dotnet build "blazorium.producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "blazorium.producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "blazorium.producer.dll"]