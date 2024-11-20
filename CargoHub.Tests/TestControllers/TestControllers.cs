using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CargoHub.Controllers;
using CargoHub.Services;
using CargoHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CargoHub.Tests;

public class ControllerTests
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