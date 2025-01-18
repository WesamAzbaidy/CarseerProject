# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY carseer.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

# Ensure the application listens on port 80 inside the container
ENV ASPNETCORE_URLS=http://+:80

# Set the entry point
ENTRYPOINT ["dotnet", "carseer.dll"]

# Expose port 80
EXPOSE 80
