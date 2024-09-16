FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/ImageNet.Api/ImageNet.Api.csproj", "ImageNet.Api/"]
COPY ["src/ImageNet.Core/ImageNet.Core.csproj", "ImageNet.Core/"]
COPY ["src/ImageNet.Infrastructure/ImageNet.Infrastructure.csproj", "ImageNet.Infrastructure/"]
RUN dotnet restore "ImageNet.Api/ImageNet.Api.csproj"

COPY src/ .
WORKDIR "/src/ImageNet.Api"
RUN dotnet build "ImageNet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageNet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageNet.Api.dll"]