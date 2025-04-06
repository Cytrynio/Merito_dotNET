using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MeritoWebApp.Controllers;
using MeritoWebApp.Models;
using Microsoft.AspNetCore.Http;

namespace MeritoWebApp.Tests;

public class HomeControllerTests
{
    private readonly Mock<ILogger<HomeController>> _mockLogger;
    private readonly HomeController _controller;

    public HomeControllerTests()
    {
        _mockLogger = new Mock<ILogger<HomeController>>();
        _controller = new HomeController(_mockLogger.Object);

        // Mock HttpContext
        var httpContext = new DefaultHttpContext();
        httpContext.TraceIdentifier = "TestTraceId";
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
    }

    [Fact]
    public void Index_Returns_ViewResult()
    {
        // Act
        var result = _controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Privacy_Returns_ViewResult()
    {
        // Act
        var result = _controller.Privacy();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Error_Returns_ViewResult_With_ErrorViewModel()
    {
        // Act
        var result = _controller.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<ErrorViewModel>(viewResult.Model);
        Assert.False(string.IsNullOrEmpty(model.RequestId));
    }
}
