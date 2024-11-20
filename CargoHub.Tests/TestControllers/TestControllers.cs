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
    public class ClientControllerTests
    {

        [Fact]
        public async Task Get_ReturnsOkResult_WhenClientExists()
        {
            // Arrange
            int clientId = 1;
            _mockService.Setup(service => service.Get(clientId));

            // Act
            var result = await _controller.Get(clientId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(clientId, okResult.Value);
        }
    }
}