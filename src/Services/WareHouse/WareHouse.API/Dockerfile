#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/WareHouse/WareHouse.API/WareHouse.API.csproj", "src/Services/WareHouse/WareHouse.API/"]
COPY ["BaseDoAnTotNghiep/BaseDoAnTotNghiep.csproj", "BaseDoAnTotNghiep/"]
COPY ["src/Services/KafKa.Net/KafKa.Net.csproj", "src/Services/KafKa.Net/"]
COPY ["src/Services/WareHouse/WareHouse.Domain/WareHouse.Domain.csproj", "src/Services/WareHouse/WareHouse.Domain/"]
COPY ["src/Services/WareHouse/WareHouse.Infrastructure/WareHouse.Infrastructure.csproj", "src/Services/WareHouse/WareHouse.Infrastructure/"]
RUN dotnet restore "src/Services/WareHouse/WareHouse.API/WareHouse.API.csproj"
COPY . .
WORKDIR "/src/src/Services/WareHouse/WareHouse.API"
RUN dotnet build "WareHouse.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WareHouse.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WareHouse.API.dll"]