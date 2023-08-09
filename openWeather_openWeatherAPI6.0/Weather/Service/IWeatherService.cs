using System;
namespace openWeather_openWeatherAPI6._0.Weather.Service
{
	public interface IWeatherService
	{
		Task<WeatherResponse> GetWeather(string location);
	}
}

