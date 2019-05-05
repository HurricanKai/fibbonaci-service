FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["Messages/Messages.csproj", "Messages/"]
COPY ["Fibbonaci-Frontend/Fibbonaci-Frontend.csproj", "Fibbonaci-Frontend/"]
RUN dotnet restore "Fibbonaci-Frontend/Fibbonaci-Frontend.csproj"
COPY . .
WORKDIR "/src/Fibbonaci-Frontend"
RUN dotnet build "Fibbonaci-Frontend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fibbonaci-Frontend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fibbonaci-Frontend.dll"]