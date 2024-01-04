#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Apps/FleetManagement.Api/FleetManagement.Api.csproj", "Apps/FleetManagement.Api/"]
RUN dotnet restore "Apps/FleetManagement.Api/FleetManagement.Api.csproj"
COPY . .
WORKDIR "/src/Apps/FleetManagement.Api"
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p Fleet007
RUN dotnet build "FleetManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=https://+:443;http://+:5210
EXPOSE 5210
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
ENTRYPOINT ["dotnet", "FleetManagement.Api.dll"]