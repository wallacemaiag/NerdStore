#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/NS.Catalog.API/NS.Catalog.API.csproj", "src/Services/NS.Catalog.API/"]
RUN dotnet restore "src/Services/NS.Catalog.API/NS.Catalog.API.csproj"
COPY . .
WORKDIR "/src/src/Services/NS.Catalog.API"
RUN dotnet build "NS.Catalog.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NS.Catalog.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NS.Catalog.API.dll"]