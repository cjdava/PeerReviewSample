using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PeerReviewSample.Controllers;
using PeerReviewSample.Models;

namespace PeerReviewSample.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _loggerMock;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        _loggerMock = new Mock<ILogger<HomeController>>();
        _controller = new HomeController(_loggerMock.Object);
    }

    [Fact]
    public void Index_ReturnsViewResult()
    {
        var result = _controller.Index();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        var result = _controller.Privacy();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Constructor_ThrowsArgumentNullException_WhenLoggerIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new HomeController(null!));
    }
}
