﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ihp-Blazor/Ihp-Blazor.csproj", "Ihp-Blazor/"]
RUN dotnet restore "Ihp-Blazor/Ihp-Blazor.csproj"
COPY . .
WORKDIR "/src/Ihp-Blazor"
RUN dotnet build "Ihp-Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ihp-Blazor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ihp-Blazor.dll"]