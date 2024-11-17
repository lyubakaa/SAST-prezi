# =======================================================
# Stage 1 - Build/compile app using container
# =======================================================
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /build

# Copy project file first to leverage Docker cache for dependencies
COPY src/*.csproj ./src/
RUN dotnet restore src/*.csproj

# Copy the rest of the source code
COPY src/. ./src/
WORKDIR /build/src
RUN dotnet publish --configuration Release -o /app

# =======================================================
# Stage 2 - Runtime image
# =======================================================
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine

# Create non-root user
RUN addgroup -S appgroup && adduser -S appuser -G appgroup

# Set working directory and change ownership
WORKDIR /app
COPY --from=build /app .

# Change ownership of the app directory to non-root user
RUN chown -R appuser:appgroup /app

# Switch to non-root user
USER appuser

# Expose port 5000 from Kestrel webserver
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "dotnet-demoapp.dll"]