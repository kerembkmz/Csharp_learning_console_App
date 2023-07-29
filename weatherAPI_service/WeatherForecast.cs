namespace weatherAPI_service;
using System.Net.Http;
using Newtonsoft.Json; //For serialization/deserialization.

public class WeatherService {
    private readonly HttpClient httpClientV;
    private readonly string apiKeyV;

    public WeatherService(HttpClient httpClient)
    {
        httpClientV = httpClient;
        apiKeyV = keys.GetWeatherAPIKey();
    }

    public async Task<WeatherData> GetWeatherData(string city) {
        var apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKeyV}";
        var response = await httpClientV.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            // Deserialize the JSON response into WeatherData.
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherData>(json);
        }

        // Return null or handle error cases.
        return null;
    }

   }


public class WeatherData
{
    public Coordinates Coord { get; set; }
    public WeatherInfo[] Weather { get; set; }
    public string Base { get; set; }
    public MainWeatherData Main { get; set; }
    public int Visibility { get; set; }
    public WindData Wind { get; set; }
    public CloudData Clouds { get; set; }
    public long Dt { get; set; }
    public SysData Sys { get; set; }
    public int Timezone { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Cod { get; set; }
}

public class Coordinates
{
    public double Lon { get; set; }
    public double Lat { get; set; }
}

public class WeatherInfo
{
    public int Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
}

public class MainWeatherData
{
    public double Temp { get; set; }
    public double FeelsLike { get; set; }
    public double TempMin { get; set; }
    public double TempMax { get; set; }
    public int Pressure { get; set; }
    public int Humidity { get; set; }
}

public class WindData
{
    public double Speed { get; set; }
    public int Deg { get; set; }
}

public class CloudData
{
    public int All { get; set; }
}

public class SysData
{
    public int Type { get; set; }
    public int Id { get; set; }
    public string Country { get; set; }
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
}