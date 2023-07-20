using System;
using Newtonsoft.Json; //Newtonsoft.Json From NuGet Packages.
//Request Library.
using System.Net;
using System.IO;

namespace Csharp_weather_app// Note: actual namespace depends on the project name.
{
    public class WeatherData {
        public string Description { get; }
        public double Temperature { get; }
        public string Location { get; }
        public int Humidity { get; }
        public double WindSpeed { get; }
          
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(API_Keys.GetWeatherAPIKey());  
            //Console.WriteLine(API_Keys.GetPositionStackAPIKey());
            //Testing to see whether the correct returns are satisfied.


            string CityInput = "";

            while (CityInput!= "q") {

                Console.WriteLine("To quit, press q");
                Console.Write("City: ");
                CityInput = Console.ReadLine();

                string jsonObj = getWhetherAPI(CityInput);

                   
             
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonObj); //Using Newtonsoft NuGet package to deserialize the json object.
                Console.WriteLine("Description: " + weatherData.Description);
                Console.WriteLine("Temperature: " + (weatherData.Temperature - 273.15));
                Console.WriteLine("Location: " + weatherData.Location);
                Console.WriteLine("Humidity: " + weatherData.Humidity);
                Console.WriteLine("Wind Speed: " + weatherData.WindSpeed);

               

            }
            
            




            
        }

        static string getWhetherAPI(string location) {
            var url = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + API_Keys.GetWeatherAPIKey();
            var request = WebRequest.Create(url);
            request.Method = "GET";

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            var data = reader.ReadToEnd();


            return data;

        }
    }
}