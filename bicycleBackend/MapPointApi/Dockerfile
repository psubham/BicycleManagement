FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MapPointApi.csproj", ""]
RUN dotnet restore "./MapPointApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "MapPointApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MapPointApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MapPointApi.dll"]