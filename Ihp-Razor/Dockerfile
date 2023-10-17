FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Ihp-Razor/Ihp-Razor.csproj", "Ihp-Razor/"]
RUN dotnet restore "Ihp-Razor/Ihp-Razor.csproj"
COPY . .
WORKDIR "/src/Ihp-Razor"
RUN dotnet build "Ihp-Razor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ihp-Razor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ihp-Razor.dll"]
