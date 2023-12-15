# Run with
# docker run -d --restart unless-stopped -p 8080:80 ihp-razor

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV TZ=Europe/Berlin

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ihp-Razor.csproj", "Ihp-Razor/"]
RUN dotnet restore "Ihp-Razor/Ihp-Razor.csproj"
COPY . Ihp-Razor/
WORKDIR "/src/Ihp-Razor"
RUN dotnet build "Ihp-Razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ihp-Razor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ihp-Razor.dll"]
