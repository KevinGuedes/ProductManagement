#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProductManagement.Api/ProductManagement.Api.csproj", "ProductManagement.Api/"]
COPY ["ProductManagement.Application/ProductManagement.Application.csproj", "ProductManagement.Application/"]
COPY ["ProductManagement.Domain/ProductManagement.Domain.csproj", "ProductManagement.Domain/"]
COPY ["ProductManagement.Infra.IoC/ProductManagement.Infra.IoC.csproj", "ProductManagement.Infra.IoC/"]
COPY ["ProductManagement.Infra.Persistence/ProductManagement.Infra.Persistence.csproj", "ProductManagement.Infra.Persistence/"]
RUN dotnet restore "ProductManagement.Api/ProductManagement.Api.csproj"
COPY . .
WORKDIR "/src/ProductManagement.Api"
RUN dotnet build "ProductManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.Api.dll"]