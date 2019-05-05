FROM mcr.microsoft.com/dotnet/core/runtime:3.0-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-stretch AS build
WORKDIR /src
COPY ["FibbonacivService/Fibbonaci Service.csproj", "Fibbonaci-Service/"]
COPY ["Messages/Messages.csproj", "Messages/"]
RUN dotnet restore "Fibbonaci-Service/Fibbonaci Service.csproj"
COPY . .
WORKDIR "/src/Fibbonaci-Service"
RUN dotnet build "Fibbonaci Service.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Fibbonaci Service.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fibbonaci Service.dll"]