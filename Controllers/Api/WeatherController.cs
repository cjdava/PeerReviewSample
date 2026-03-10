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
        private readonly WeatherService _weatherService;

        public WeatherController()
        {
            _weatherService = new WeatherService();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherService.GetForecasts();
        }
    }
}