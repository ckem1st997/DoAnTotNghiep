#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Master/Master/Master.csproj", "src/Services/Master/Master/"]
COPY ["src/Services/Master/Infrastructure/Infrastructure.csproj", "src/Services/Master/Infrastructure/"]
COPY ["src/Services/Base/Base/Base.csproj", "src/Services/Base/Base/"]
COPY ["src/Services/KafKa.Net/KafKa.Net.csproj", "src/Services/KafKa.Net/"]
RUN dotnet restore "src/Services/Master/Master/Master.csproj"
COPY . .
WORKDIR "/src/src/Services/Master/Master"
RUN dotnet build "Master.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Master.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Master.dll"]