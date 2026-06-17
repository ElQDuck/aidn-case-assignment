FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled AS base
WORKDIR /app
EXPOSE 5171

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["NewsScore.Api/NewsScore.Api.csproj", "NewsScore.Api/"]
COPY ["NewsScore.BusinessLogic/NewsScore.BusinessLogic.csproj", "NewsScore.BusinessLogic/"]
RUN dotnet restore "NewsScore.Api/NewsScore.Api.csproj"
COPY . .
RUN dotnet publish "NewsScore.Api/NewsScore.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NewsScore.Api.dll"]