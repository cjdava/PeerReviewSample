using Xunit;
using PeerReviewSample.Application;
using PeerReviewSample.Models;

namespace PeerReviewSample.Tests.Application;

public class WeatherServiceTests
{
    private readonly WeatherService _sut = new();

    private static readonly string[] ValidSummaries = { "Sunny", "Cloudy", "Rainy", "Windy", "Snowy" };

    [Fact]
    public void GetForecasts_ReturnsExactlyFiveForecasts()
    {
        var result = _sut.GetForecasts().ToList();

        Assert.Equal(5, result.Count);
    }

    [Fact]
    public void GetForecasts_EachForecastDateIsWithinExpectedRange()
    {
        var before = DateTime.Now.Date;
        var result = _sut.GetForecasts().ToList();
        var after = DateTime.Now.Date.AddDays(5);

        for (int i = 0; i < result.Count; i++)
        {
            var expectedDate = before.AddDays(i);
            Assert.True(
                result[i].Date.Date >= expectedDate && result[i].Date.Date <= after,
                $"Forecast at index {i} has unexpected date {result[i].Date}");
        }
    }

    [Fact]
    public void GetForecasts_AllTemperaturesAreWithinExpectedRange()
    {
        var result = _sut.GetForecasts();

        foreach (var forecast in result)
        {
            Assert.InRange(forecast.TemperatureC, -10, 34);
        }
    }

    [Fact]
    public void GetForecasts_AllSummariesAreFromValidSet()
    {
        var result = _sut.GetForecasts();

        foreach (var forecast in result)
        {
            Assert.Contains(forecast.Summary, ValidSummaries);
        }
    }

    [Fact]
    public void GetForecasts_NoSummaryIsNullOrEmpty()
    {
        var result = _sut.GetForecasts();

        foreach (var forecast in result)
        {
            Assert.False(string.IsNullOrEmpty(forecast.Summary));
        }
    }

    [Fact]
    public void GetForecasts_ReturnsDifferentResultsOnConsecutiveCalls()
    {
        // Over many calls, random values should not always be identical
        var results = Enumerable.Range(0, 20)
            .Select(_ => _sut.GetForecasts().Select(f => f.TemperatureC).ToList())
            .ToList();

        // At least two calls should differ (probability of all 20 being identical is negligible)
        Assert.True(
            results.Any(r => !r.SequenceEqual(results[0])),
            "GetForecasts always returns the same temperatures, suggesting randomness is broken.");
    }
}
