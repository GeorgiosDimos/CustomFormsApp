# Use the .NET Core SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

LABEL author="Dan Wahlin"

# ENV ASPNETCORE_URLS=http://+:5099

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# Copy everything else and build the app
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "CustomFormsApp.dll"]
