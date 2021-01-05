#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Cqrs.Application/Cqrs.Application.csproj", "Cqrs.Application/"]
COPY ["Cqrs.Infra.CrossCutting/Cqrs.Infra.CrossCutting.csproj", "Cqrs.Infra.CrossCutting/"]
COPY ["Cqrs.Infra.Data/Cqrs.Infra.Data.csproj", "Cqrs.Infra.Data/"]
COPY ["Cqrs.Domain/Cqrs.Domain.csproj", "Cqrs.Domain/"]
COPY ["Cqrs.Service/Cqrs.Service.csproj", "Cqrs.Service/"]
RUN dotnet restore "Cqrs.Application/Cqrs.Application.csproj"
COPY . .
WORKDIR "/src/Cqrs.Application"
RUN dotnet build "Cqrs.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cqrs.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cqrs.Application.dll"]
