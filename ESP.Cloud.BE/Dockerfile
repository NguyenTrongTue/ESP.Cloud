FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ESP.Cloud.BE.Application/ESP.Cloud.BE.Application.csproj", "ESP.Cloud.BE.Application/"]
COPY ["ESP.Cloud.BE.Cache/ESP.Cloud.BE.Cache.csproj", "ESP.Cloud.BE.Cache/"]
COPY ["ESP.Cloud.BE.ChatGPT/ESP.Cloud.BE.ChatGPT.csproj", "ESP.Cloud.BE.ChatGPT/"]
COPY ["ESP.Cloud.BE.Core/ESP.Cloud.BE.Core.csproj", "ESP.Cloud.BE.Core/"]
COPY ["ESP.Cloud.BE.ElasticSearch/ESP.Cloud.BE.ElasticSearch.csproj", "ESP.Cloud.BE.ElasticSearch/"]
COPY ["ESP.Cloud.BE.Email/ESP.Cloud.BE.Email.csproj", "ESP.Cloud.BE.Email/"]
COPY ["ESP.Cloud.BE.GoogleAPI/ESP.Cloud.BE.GoogleAPI.csproj", "ESP.Cloud.BE.GoogleAPI/"]
COPY ["ESP.Cloud.BE.Host/ESP.Cloud.BE.Host.csproj", "ESP.Cloud.BE.Host/"]
COPY ["ESP.Cloud.BE.Infrastructure/ESP.Cloud.BE.Infrastructure.csproj", "ESP.Cloud.BE.Infrastructure/"]
COPY ["ESP.Cloud.BE.Jobs/ESP.Cloud.BE.Jobs.csproj", "ESP.Cloud.BE.Jobs/"]
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

EXPOSE 5000
ENTRYPOINT ["dotnet", "ESP.Cloud.BE.Host.dll"]