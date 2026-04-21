# STAGE 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the .csproj file and restore dependencies (layer caching)
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining files and build the release
COPY . ./
RUN dotnet publish -c Release -o out

# STAGE 2: Run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy the build output from the first stage
COPY --from=build-env /app/out .

# Expose the port your app runs on (usually 80 or 8080 in .NET 8)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Start the app (Replace 'YourAppName' with your actual DLL name)
ENTRYPOINT ["dotnet", "BM-F1-WorkshopInventory.dll"]