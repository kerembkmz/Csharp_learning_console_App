using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForecastApp.ForecastAppModels;
using ForecastApp.OpenWeatherMapModels;
using ForecastApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForecastAppController : ControllerBase
    {
        private readonly IForecastRepository _forecastRepository;

        // Dependency Injection
        public ForecastAppController(IForecastRepository forecastAppRepo)
        {
            _forecastRepository = forecastAppRepo;
        }

        


        // GET: api/ForecastApp/City
        [HttpGet("{city}")]
        public IActionResult City(string city)
        {
            // Consume the OpenWeatherAPI in order to bring Forecast data in our page.
            WeatherResponse weatherResponse = _forecastRepository.GetForecast(city);
            if (weatherResponse != null)
            {
                var viewModel = new City
                {
                    Name = weatherResponse.Name,
                    Humidity = weatherResponse.Main.Humidity,
                    Pressure = weatherResponse.Main.Pressure,
                    Temp = weatherResponse.Main.Temp,
                    Weather = weatherResponse.Weather[0].Main,
                    Wind = weatherResponse.Wind.Speed
                };
                return Ok(viewModel);
            }
            return NotFound();
        }
    }
}