using System;
using Newtonsoft.Json;

namespace openWeather_openWeatherAPI6._0.Weather.Service.ServiceAbstracts
{
	public static class GetWeatherDataHelper
	{
        public static async Task<WeatherResponse> GetWeatherData(IHttpClientFactory httpClientFactory,string url)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(url);
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

