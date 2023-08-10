using System;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

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

            try
            {
                string getWeatherInfoViaLocationUrl = $"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}";
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(getWeatherInfoViaLocationUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(content);
                return weatherData;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error fetching weather data, error:" + ex.Message);
                throw;
            }
        }
    }
}

