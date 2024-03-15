# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /ParkingAppWebApi

COPY . .

RUN	
# Serve Stage