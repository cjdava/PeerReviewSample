using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PeerReviewSample.Models;

namespace PeerReviewSample.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var summaries = new[] { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };

            var forecasts = new List<WeatherForecast>();
            for (int i = 0; i < 5; i++)
            {
                forecasts.Add(new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(i),
                    TemperatureC = rng.Next(-10, 35),
                    Summary = summaries[rng.Next(summaries.Length)]
                });
            }
            return forecasts;
        }
    }
}