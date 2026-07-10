FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# --------------- #
# ---- build ---- #
# --------------- #
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["TelerikCRM.Datasync.Server/TelerikCRM.Datasync.Server.csproj", "TelerikCRM.Datasync.Server/"]
# Remove nuget.telerik.com from the nuget.config to skip needing authentication (no Telerik packages are used here)
RUN dotnet nuget remove source "Telerik_Feed" --configfile "./nuget.config"
# Now, we can restore packages from api.nuget.org
RUN dotnet restore "TelerikCRM.Datasync.Server/TelerikCRM.Datasync.Server.csproj" --configfile "./nuget.config"
COPY . .
WORKDIR "/src/TelerikCRM.Datasync.Server"
# Use --no-restore because we've explicitly restored packages already
RUN dotnet build "TelerikCRM.Datasync.Server.csproj" -c Release -o /app/build --no-restore


# ----------------- #
# ---- publish ---- #
# ----------------- #
FROM build AS publish
# Use --no-restore because we've explicitly restored packages already
RUN dotnet publish "TelerikCRM.Datasync.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore


# --------------- #
# ---- final ---- #
# --------------- #
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TelerikCRM.Datasync.Server.dll"]