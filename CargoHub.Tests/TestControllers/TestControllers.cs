using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using CargoHub.Models;

namespace CargoHub.Tests
{
    public class ClientControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ClientControllerTests(WebApplicationFactory<Program> factory)
        {
            // Create a test client and inject the DbContext
            _client = factory.CreateClient();
            _factory = factory;
        }

        private void WriteLogToFile(string filePath, string message)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(message);
            }
        }

        [Fact]
        public async Task Get_ClientById_ReturnsClientDetails()
        {
            // Arrange
            var clientId = 1;
            var endpoint = $"/api/v1/client/{clientId}";
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            // Act
            var response = await _client.GetAsync(endpoint);

            stopwatch.Stop();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"id\":1", responseBody);


            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                // Access the database and fetch the client with the same ID
                var clientFromDb = await context.Client.FindAsync(clientId);
                Assert.NotNull(clientFromDb); // Assert that client exists in the DB
                Assert.Equal(clientId, clientFromDb.Id); // Compare the id
                // Log the response body, execution time, and database comparison to a file
                string logFilePath = "C:/CargoHub2/CargoHub/CargoHub.Tests/TestControllers/Test_Results/test_results.txt";
                string logMessage = $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n" +
                                    $"API Response Body: {responseBody}\n" +
                                    $"Database Client Name: {clientFromDb.Name}\n" +
                                    $"Database Client ID: {clientFromDb.Id}\n\n";

                // Write log to the file
                WriteLogToFile(logFilePath, logMessage);
            }
        }
    }
}
