# Use official .NET SDK image to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the port Render will assign
ENV ASPNETCORE_URLS=http://+:$PORT

# Start the app
ENTRYPOINT ["dotnet", "WebviAPI.dll"]
