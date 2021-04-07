#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/NS.Bff.Purchases/NS.Bff.Purchases.csproj", "src/ApiGateways/NS.Bff.Purchases/"]
RUN dotnet restore "src/ApiGateways/NS.Bff.Purchases/NS.Bff.Purchases.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/NS.Bff.Purchases"
RUN dotnet build "NS.Bff.Purchases.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NS.Bff.Purchases.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NS.Bff.Purchases.dll"]