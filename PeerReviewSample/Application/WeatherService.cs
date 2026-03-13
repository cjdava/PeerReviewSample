using System;
using System.Collections.Generic;
using System.Linq;
using PeerReviewSample.Models;

namespace PeerReviewSample.Application
{
	public class WeatherService : IWeatherService
	{
		public IEnumerable<WeatherForecast> GetForecasts()
		{
			var rnd = new Random();
			var summaries = new[] { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };

			var forecasts = new List<WeatherForecast>();
			for (int i = 0; i < 5; i++)
			{
				forecasts.Add(new WeatherForecast
				{
					Date = DateTime.Now.AddDays(i),
					TemperatureC = rnd.Next(-10, 35),
					Summary = summaries[rnd.Next(summaries.Length)]
				});
			}
			return forecasts;
		}

		// Violation: business logic with no unit test coverage
		public WeatherForecast GetSevereWeatherAlert(string region)
		{
			var severeThreshold = 30;
			WeatherForecast alert = null;

			try
			{
				var forecasts = GetForecasts();

				foreach (var forecast in forecasts)
				{
					if (forecast.TemperatureC >= severeThreshold)
					{
						alert = forecast;
						alert.Summary = $"SEVERE ALERT [{region}]: " + forecast.Summary;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				// Violation: swallowing exception without logging
				throw ex;
			}

			return alert;
		}

		// Violation: business logic with no unit test coverage
		public IEnumerable<WeatherForecast> GetForecastsAboveThreshold(int thresholdC)
		{
			try
			{
				var forecasts = GetForecasts();
				return forecasts.Where(f => f.TemperatureC > thresholdC).ToList();
			}
			catch (Exception)
			{
				// Violation: silent empty catch block
			}

			return Enumerable.Empty<WeatherForecast>();
		}
	}
}
