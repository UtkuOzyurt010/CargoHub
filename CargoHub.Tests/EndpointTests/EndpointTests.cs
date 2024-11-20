using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using static TestHelperFunctions;
using CargoHub.Models;

namespace CargoHub.Tests
{
    public class EndpointTestsTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string LogFilePath = $"C:/CargoHub2/CargoHub/CargoHub.Tests/EndpointTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        public EndpointTestsTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Get_ClientById_ReturnsClientDetails()
        {
            var clientId = 1;
            var endpoint = $"/api/v1/client/{clientId}";
            var stopwatch = new Stopwatch();

            // Create instance of UnitTest to use its method
            var unitTests = new UnitTests(_factory);

            stopwatch.Start();

            // Act
            var response = await _client.GetAsync(endpoint);

            stopwatch.Stop();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("\"id\":1", responseBody);

            Client clientFromDb = default;
            try
            {
                clientFromDb = await unitTests.GetClientByIdFromDb(clientId);
            }
            catch (Exception ex)
            {
                string logMessageError = $"Error retrieving client with ID {clientId}: {ex.Message}\nStack Trace: {ex.StackTrace}";
                WriteLogToFile(LogFilePath, logMessageError);
            }

            // Now check if clientFromDb is null before accessing its properties
            if (clientFromDb != null)
            {
                Assert.Equal(clientId, clientFromDb.Id);
                string logMessage = $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n" +
                                    $"API Response Body: {responseBody}\n" +
                                    $"Database Client Name: {clientFromDb.Name}\n" +
                                    $"Database Client ID: {clientFromDb.Id}\n\n";
                WriteLogToFile(LogFilePath, logMessage);
            }
            else
            {
                string logMessageError = "Client data could not be retrieved from the database.";
                WriteLogToFile(LogFilePath, logMessageError);
            }
        }
    }
}
