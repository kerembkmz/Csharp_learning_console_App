using System;
using System.Diagnostics;
using Newtonsoft.Json; //Newtonsoft.Json From NuGet Packages.

using System.Net.Http; //To Use Http Client
using System.Threading.Tasks;

//Request Library.
using System.Net;
using System.IO;
using Csharp_weather_app// Note: actual namespace depends on the project name.
;


//This way, HttpClient is instantiated once per application, rather than per-use
//So it is more efficient.
HttpClient client = new HttpClient();

async Task<string> getWeatherAPIUsingHTTPClient(string location)
{
    using var client = new HttpClient();
    var url = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + API_Keys.GetWeatherAPIKey();
    var response = await client.GetAsync(url);
    return await response.Content.ReadAsStringAsync();
}

//Console.WriteLine(API_Keys.GetWeatherAPIKey());  
//Console.WriteLine(API_Keys.GetPositionStackAPIKey());
//Testing to see whether the correct returns are satisfied.


string CityInput = "";

while (CityInput != "q")
{

    Console.WriteLine("To quit, press q");
    Console.Write("City: ");
    CityInput = Console.ReadLine();

    //Stopwatch stopwatch1 = new Stopwatch();
    //Stopwatch stopwatch2 = new Stopwatch();

    //stopwatch1.Start();
    //string jsonObj2 = await getWeatherAPIUsingHTTPClient(CityInput);
    //Console.WriteLine(jsonObj2);
    //stopwatch1.Stop();

    //Console.WriteLine("Time using HTTP: " + stopwatch1.ElapsedMilliseconds);


    //stopwatch2.Start();
    string jsonObj = getWeatherAPI(CityInput);
    //Console.WriteLine(jsonObj);
    //stopwatch2.Stop();

    //Console.WriteLine("Time using webRequest: " + stopwatch2.ElapsedMilliseconds);


    var weatherData = JsonConvert.DeserializeObject<dynamic>(jsonObj); //Using Newtonsoft NuGet package to deserialize the json object.
                                                                       //Making deserializing to a dynamic type so that it is easier to read & grasp the code.
                                                                       //Also solves the non-nullable run.

    Console.WriteLine("Description: " + weatherData.weather[0].description);
    Console.WriteLine("Temperature: " + (weatherData.main.temp - 273.15));
    Console.WriteLine("Location: " + weatherData.name);
    Console.WriteLine("Humidity: " + weatherData.main.humidity);
    Console.WriteLine("Wind Speed: " + weatherData.wind.speed);

}

string getWeatherAPI(string location)
{
    var url = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + API_Keys.GetWeatherAPIKey();
    var request = WebRequest.Create(url);
    request.Method = "GET";

    using var webResponse = request.GetResponse();
    using var webStream = webResponse.GetResponseStream();

    using var reader = new StreamReader(webStream);
    var data = reader.ReadToEnd();


    return data;

}

