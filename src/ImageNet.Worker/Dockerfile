FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/ImageNet.Worker/ImageNet.Worker.csproj", "ImageNet.Worker/"]
COPY ["src/ImageNet.Core/ImageNet.Core.csproj", "ImageNet.Core/"]
COPY ["src/ImageNet.Infrastructure/ImageNet.Infrastructure.csproj", "ImageNet.Infrastructure/"]
RUN dotnet restore "ImageNet.Worker/ImageNet.Worker.csproj"

COPY src/ .
WORKDIR "/src/ImageNet.Worker"
RUN dotnet build "ImageNet.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ImageNet.Worker.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ImageNet.Worker.dll"]