# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy solution and project files
COPY ["QuantityMeasurementApp.sln", "."]
COPY ["src/QuantityMeasurementApp.Api/QuantityMeasurementApp.Api.csproj", "src/QuantityMeasurementApp.Api/"]
COPY ["src/QuantityMeasurementApp.Business/QuantityMeasurementApp.Business.csproj", "src/QuantityMeasurementApp.Business/"]
COPY ["src/QuantityMeasurementApp.Models/QuantityMeasurementApp.Models.csproj", "src/QuantityMeasurementApp.Models/"]
COPY ["src/QuantityMeasurementApp.Repository/QuantityMeasurementApp.Repository.csproj", "src/QuantityMeasurementApp.Repository/"]
COPY ["src/QuantityMeasurementController/QuantityMeasurementApp.Controller.csproj", "src/QuantityMeasurementController/"]
COPY ["QuantityMeasurementApp.Repository/QuantityMeasurementApp.Repository.csproj", "QuantityMeasurementApp.Repository/"]

# Restore dependencies
RUN dotnet restore "QuantityMeasurementApp.sln"

# Copy entire source
COPY . .

# Build the API project
RUN dotnet build "src/QuantityMeasurementApp.Api/QuantityMeasurementApp.Api.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "src/QuantityMeasurementApp.Api/QuantityMeasurementApp.Api.csproj" -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

# Copy published files
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production
ENV APP_MODE=api

# Expose port
EXPOSE 5000

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=40s --retries=3 \
    CMD ["curl", "-f", "http://localhost:5000/health", "||", "exit", "1"]

# Run the application
ENTRYPOINT ["dotnet", "QuantityMeasurementApp.Api.dll"]
