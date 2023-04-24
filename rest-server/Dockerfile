# Use the official .NET SDK image as a build environment.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory.
WORKDIR /app

# Copy the project file and restore dependencies.
COPY *.csproj ./


RUN dotnet restore

# Copy the source code and build the application.
COPY . ./
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime image as a base image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory.
WORKDIR /app

# Copy the published application from the build environment.
COPY --from=build-env /app/out .

# Expose the HTTP port.
EXPOSE 80

# Run the application when the container starts.
ENTRYPOINT ["dotnet", "rest-server.dll"]