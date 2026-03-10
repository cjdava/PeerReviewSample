using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PeerReviewSample.Models;
using PeerReviewSample.Application;

namespace PeerReviewSample.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherService.GetForecasts();
        }
    }
}