using System;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using openWeather_openWeatherAPI6._0.Weather.Service.ServiceAbstracts;

namespace openWeather_openWeatherAPI6._0.Weather.Service
{
	public class WeatherService : IWeatherService
	{
        private IHttpClientFactory _httpClientFactory;
        string _apiKey;

        public WeatherService(IHttpClientFactory httpClientFactory)
		{
            _httpClientFactory = httpClientFactory;

            _apiKey = Keys.getAPIKey(); 
        }

        public async Task<WeatherResponse> GetWeather(string location)
        {
            string getWeatherInfoViaLocationUrl = $"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}";
            return await GetWeatherDataHelper.GetWeatherData(_httpClientFactory, getWeatherInfoViaLocationUrl);

        }
    }
}

