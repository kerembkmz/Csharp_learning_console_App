#Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./openWeather_openWeatherAPI6.0/openWeather_openWeatherAPI6.0.csproj" --disable-parallel
RUN dotnet publish "./openWeather_openWeatherAPI6.0/openWeather_openWeatherAPI6.0.csproj" -c release -o /app --no-restore



#Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5002

ENTRYPOINT ["dotnet", "openWeather_openWeatherAPI6.0.dll"]