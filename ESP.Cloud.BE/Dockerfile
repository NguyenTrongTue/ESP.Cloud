FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ESP.Cloud.BE.Cache/ESP.Cloud.BE.Cache.csproj", "ESP.Cloud.BE.Cache/"]
COPY ["ESP.Cloud.BE.Jobs/ESP.Cloud.BE.Jobs.csproj", "ESP.Cloud.BE.Jobs/"]
COPY ["ESP.Cloud.BE.Email/ESP.Cloud.BE.Email.csproj", "ESP.Cloud.BE.Email/"]
COPY ["ESP.Cloud.BE.Infrastructure/ESP.Cloud.BE.Infrastructure.csproj", "ESP.Cloud.BE.Infrastructure/"]
COPY ["ESP.Cloud.BE.Core/ESP.Cloud.BE.Core.csproj", "ESP.Cloud.BE.Core/"]
COPY ["ESP.Cloud.BE.Application/ESP.Cloud.BE.Application.csproj", "ESP.Cloud.BE.Application/"]
COPY ["ESP.Cloud.BE.Host/ESP.Cloud.BE.Host.csproj", "ESP.Cloud.BE.Host/"]
RUN dotnet restore "./ESP.Cloud.BE.Host/ESP.Cloud.BE.Host.csproj"
COPY . .
WORKDIR "/src/ESP.Cloud.BE.Host"
RUN dotnet build "./ESP.Cloud.BE.Host.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ESP.Cloud.BE.Host.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:5000

EXPOSE 5000
ENTRYPOINT ["dotnet", "ESP.Cloud.BE.Host.dll"]