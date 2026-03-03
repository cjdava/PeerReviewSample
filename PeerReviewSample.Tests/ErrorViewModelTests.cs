using PeerReviewSample.Models;

namespace PeerReviewSample.Tests;

public class ErrorViewModelTests
{
    [Fact]
    public void IsRequestIdVisible_WhenRequestIdIsSet_ReturnsTrue()
    {
        var model = new ErrorViewModel { RequestId = "abc-123" };

        Assert.True(model.IsRequestIdVisible);
    }

    [Fact]
    public void IsRequestIdVisible_WhenRequestIdIsNull_ReturnsFalse()
    {
        var model = new ErrorViewModel { RequestId = null };

        Assert.False(model.IsRequestIdVisible);
    }

    [Fact]
    public void IsRequestIdVisible_WhenRequestIdIsEmpty_ReturnsFalse()
    {
        var model = new ErrorViewModel { RequestId = string.Empty };

        Assert.False(model.IsRequestIdVisible);
    }
}
