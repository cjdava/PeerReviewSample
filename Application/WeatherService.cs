using System;
using System.Collections.Generic;
using PeerReviewSample.Models;

namespace PeerReviewSample.Application
{
	public class WeatherService
	{
		public IEnumerable<WeatherForecast> GetForecasts()
		{
			var RND = new Random();
			var summaries = new[] { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };

			var forecasts = new List<WeatherForecast>();
			for (int i = 0; i < 5; i++)
			{
				forecasts.Add(new WeatherForecast
				{
					Date = DateTime.Now.AddDays(i),
					TemperatureC = RND.Next(-10, 35),
					Summary = summaries[RND.Next(summaries.Length)]
				});
			}
			return forecasts;
		}
	}
}
