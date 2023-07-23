#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5216

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NTU.IoT.Web/NTU.IoT.Web.csproj", "NTU.IoT.Web/"]
COPY ["NTU.IoT.DataAccess/NTU.IoT.DataAccess.csproj", "NTU.IoT.DataAccess/"]
COPY ["NTU.IoT.Models/NTU.IoT.Models.csproj", "NTU.IoT.Models/"]
COPY ["NTU.IoT.Utility/NTU.IoT.Utility.csproj", "NTU.IoT.Utility/"]
RUN dotnet restore "NTU.IoT.Web/NTU.IoT.Web.csproj"
COPY . .
WORKDIR "/src/NTU.IoT.Web"
RUN dotnet build "NTU.IoT.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NTU.IoT.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NTU.IoT.Web.dll", "urls=http://0.0.0.0:2377"]
