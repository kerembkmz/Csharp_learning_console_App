using System;
using System.Diagnostics;
using Newtonsoft.Json; //Newtonsoft.Json From NuGet Packages.

using System.Net.Http; //To Use Http Client
using System.Threading.Tasks;

//Request Library.
using System.Net;
using System.IO;
using Csharp_weather_app;// Note: actual namespace depends on the project name.

using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;



//This way, HttpClient is instantiated once per application, rather than per-use
//So it is more efficient on paper.


HttpClientHandler handler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
};

HttpClient client = new HttpClient(handler);

async Task<string> getWeatherAPIUsingHTTPClient(string location)
{
    //var url = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + All_Keys.GetWeatherAPIKey();
    //var url = "https://c49f93e3-f002-4bff-9e67-175b36c66809.mock.pstmn.io" + "?q" + location + "&appid=" + All_Keys.GetWeatherAPIKey();
    //var url = "https://42a4ae73-9598-47c4-9e27-ac0791695b0b.mock.pstmn.io" + "?q" + location + "&appid=" + All_Keys.GetWeatherAPIKey();
    var url = "http://localhost/api/openWeatherAPI/" + location;
    var response = await client.GetAsync(url);
    return await response.Content.ReadAsStringAsync();
}

//Console.WriteLine(API_Keys.GetWeatherAPIKey());  
//Console.WriteLine(API_Keys.GetPositionStackAPIKey());
//Testing to see whether the correct returns are satisfied.


string CityInput = "";
string choice = "";

while (CityInput != "q" || choice != "q")
{

    Console.WriteLine("To quit, press q");
    Console.WriteLine("To sign in, ype Sign In");
    Console.WriteLine("To sign up, type Sign Up");


    choice = Console.ReadLine();
    DB_Conn connection = new DB_Conn();

    if (choice == "Sign In") {
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();
        

        if (connection.CheckValidationUser(username, password))
        {


            while (CityInput != "q") {
                Console.Write("City: ");
                CityInput = Console.ReadLine();


                //Stopwatch stopwatch1 = new Stopwatch();
                //Stopwatch stopwatch2 = new Stopwatch();

                //stopwatch1.Start();
                string jsonObj = await getWeatherAPIUsingHTTPClient(CityInput);
                //Console.WriteLine(jsonObj2);
                //stopwatch1.Stop();

                //Console.WriteLine("Time using HTTP: " + stopwatch1.ElapsedMilliseconds);


                //stopwatch2.Start();
                //string jsonObj = getWeatherAPI(CityInput);
                Console.WriteLine(jsonObj);
                //stopwatch2.Stop();

                //Console.WriteLine("Time using webRequest: " + stopwatch2.ElapsedMilliseconds);


                var weatherData = JsonConvert.DeserializeObject<dynamic>(jsonObj); //Using Newtonsoft NuGet package to deserialize the json object.
                                                                                   //Making deserializing to a dynamic type so that it is easier to read & grasp the code.
                                                                                   //Also solves the non-nullable run.

                Console.WriteLine("Server: " + weatherData.weather[0].server);
                Console.WriteLine("Description: " + weatherData.weather[0].description);
                Console.WriteLine("Temperature: " + (weatherData.main.temp - 273.15));
                Console.WriteLine("Location: " + weatherData.name);
                Console.WriteLine("Humidity: " + weatherData.main.humidity);
                Console.WriteLine("Wind Speed: " + weatherData.wind.speed);
            }

        }
        else {
            Console.WriteLine("Please try again, wrong credentials.");
        }
        
    }
    else if (choice == "Sign Up") // Add the "Sign Up" option and implement sign-up functionality
    {
        Console.Write("New Username: ");
        string newUsername = Console.ReadLine();
        Console.Write("New Password: ");
        string newPassword = Console.ReadLine();

        connection.RegisterUser(newUsername, newPassword);

        Console.WriteLine("User registered successfully!");
        Console.WriteLine("Welcome to the weather app!");

        while (CityInput != "q")
        {
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
            string jsonObj = await getWeatherAPIUsingHTTPClient(CityInput);
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
    }







}


//string getWeatherAPI(string location)
//{

    //var url = "http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + All_Keys.GetWeatherAPIKey();
    //var url = "http://localhost:5135/api/openWeatherAPI/" + location; 
    //var url = "https://54c42091-68dc-4c8e-8c04-057eb70dbb5a.mock.pstmn.io" + "?q=" + location + "&appid=" + All_Keys.GetWeatherAPIKey(); ;
    //var request = WebRequest.Create(url);
    //request.Method = "GET";

 //   using var webResponse = request.GetResponse();
   // using var webStream = webResponse.GetResponseStream();

//    using var reader = new StreamReader(webStream);
  //  var data = reader.ReadToEnd();


    //return data;

//}

