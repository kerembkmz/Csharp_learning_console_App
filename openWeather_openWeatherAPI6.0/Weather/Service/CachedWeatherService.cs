using System;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace openWeather_openWeatherAPI6._0.Weather.Service
{
	public class CachedWeatherService : IWeatherService
	{
        private IHttpClientFactory _httpClientFactory;
        string _apiKey;
		private IMemoryCache _cache;
		

        public CachedWeatherService(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
		{
			_httpClientFactory = httpClientFactory;
			_cache = memoryCache;
			_apiKey = Keys.getAPIKey();
			
		}

		public async Task<WeatherResponse> GetWeather(string location) {

            //https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.caching.memory.memorycache.trygetvalue?view=dotnet-plat-ext-7.0
			//The _cache.TryGetValue's documentation is on the link above.
            if (_cache.TryGetValue(location, out WeatherResponse cachedWeather)) {
				
				
				return cachedWeather;
			}

			try
			{
				
                string getWeatherInfoViaLocationUrl = $"http://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}";
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(getWeatherInfoViaLocationUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var weatherData = JsonConvert.DeserializeObject<WeatherResponse>(content);

                //https://learn.microsoft.com/en-us/dotnet/api/system.runtime.caching.memorycache.set?view=dotnet-plat-ext-7.0
                //The _cache.Set's documentation is on the link above.
                _cache.Set(location, weatherData, DateTimeOffset.UtcNow.AddMinutes(10));

                return weatherData;
			}
			catch (HttpRequestException ex)
			{

				throw ex;
			}
		}
	}
}

