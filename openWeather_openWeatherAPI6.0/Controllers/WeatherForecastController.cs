using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Net.Http;

using openWeather_openWeatherAPI6._0;
using openWeather_openWeatherAPI6._0.Weather.Service;
using openWeather_openWeatherAPI6._0.Weather;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace openWeather_openWeatherAPI6._0.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class openWeatherAPIController : ControllerBase
    {
        
        private readonly IWeatherService _weatherService;


        public openWeatherAPIController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{location}")]
        public async Task<IActionResult> GetWeather(string location)
        {
            var weatherResponse = await _weatherService.GetWeather(location);
            if (weatherResponse != null)
            {
                return Ok(weatherResponse);
            }
            else {
                return NotFound();
            }
        }
    }
}

