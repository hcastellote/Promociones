#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Promociones/Promociones.csproj", "Promociones/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Models/Models.csproj", "Models/"]
COPY ["Helpers/Helpers.csproj", "Helpers/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "Promociones/Promociones.csproj"
COPY . .
WORKDIR "/src/Promociones"
RUN dotnet build "Promociones.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Promociones.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Promociones.dll"]