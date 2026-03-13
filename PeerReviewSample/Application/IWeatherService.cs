using System.Collections.Generic;
using PeerReviewSample.Models;

namespace PeerReviewSample.Application
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForecasts();
        WeatherForecast GetSevereWeatherAlert(string region);
        IEnumerable<WeatherForecast> GetForecastsAboveThreshold(int thresholdC);
    }
}
