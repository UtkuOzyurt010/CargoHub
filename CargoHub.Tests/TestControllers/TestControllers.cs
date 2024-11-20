using Xunit;
using api.Controllers;
using api.Services;

namespace CargoHub.Tests;

public class HomeControllerTests
{
    [Fact]
    public void Index_ReturnsExpectedResult()
    {
        var clientservice = new ClientService();
        // Arrange
        var controller = new ClientController();

        // Act
        var result = controller.Index();

        // Assert
        Assert.NotNull(result);
    }
}