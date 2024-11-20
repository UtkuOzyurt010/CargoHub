using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using static TestHelperFunctions;
using CargoHub.Models;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace CargoHub.Tests
{
    public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly int TestId = 1;

        public EndpointTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task Test_All_Id_Endpoints()
        {
            var endpointsWithIds = new List<string>
            {
                "/api/v1/client",
                "/api/v1/warehouses",
                "/api/v1/locations",
                "/api/v1/transfers",
                "/api/v1/items",
                "/api/v1/item_lines",
                "/api/v1/item_groups",
                "/api/v1/item_types",
                "/api/v1/inventories",
                "/api/v1/suppliers",
                "/api/v1/orders",
                "/api/v1/shipments",
            };

            foreach (var endpoint in endpointsWithIds)
            {
                await Get_One_ById(endpoint, TestId);
            }
        }

        public async Task Get_One_ById(string endpoint, int Id)
        {
            string LogFilePath = $"C:/CargoHub2/CargoHub/CargoHub.Tests/EndpointTests/Test_Results/Endpoint:{endpoint.Split("/").Last()}-{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string fullendpoint = $"{endpoint}/{Id}";

            var stopwatch = new Stopwatch();

            // Create instance of UnitTest to use its method
            var unitTests = new UnitTests(_factory);

            stopwatch.Start();

            // Getresponse from endpoint
            var response = await _client.GetAsync(fullendpoint);

            stopwatch.Stop();

            // Assert portion
            try
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var responseBodys = await response.Content.ReadAsStringAsync();
                    string logMessageError = $"Error on endpoint {fullendpoint} with status {response.StatusCode}\n" +
                                            $"Response Body: {responseBodys}";
                    WriteLogToFile(LogFilePath, logMessageError);
                    throw new Exception(logMessageError);
                }
                //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
            catch (Exception ex)
            {
                string logMessageError = $"Assert failed: {ex.Message}\nStack Trace: {ex.StackTrace}";
                WriteLogToFile(LogFilePath, logMessageError);
                throw;
            }

            //examine response body
            var responseBody = await response.Content.ReadAsStringAsync();

            try
            {
                Assert.Contains("\"id\":1", responseBody);
            }
            catch (Exception ex)
            {
                string logMessageError = $"Assert failed: {ex.Message}\nStack Trace: {ex.StackTrace}";
                WriteLogToFile(LogFilePath, logMessageError);
                throw;
            }
            

            Client clientFromDb = default;
            try
            {
                clientFromDb = await unitTests.GetByIdFromDb(Id);
            }
            catch (Exception ex)
            {
                string logMessageError = $"Error retrieving client with ID {Id}: {ex.Message}\nStack Trace: {ex.StackTrace}";
                WriteLogToFile(LogFilePath, logMessageError);
            }

            // Now check if clientFromDb is null before accessing its properties
            if (clientFromDb != null)
            {
                try
                {
                    Assert.Equal(Id, clientFromDb.Id);
                }
                catch (Exception ex)
                {
                    string logMessageError = $"Error retrieving client with ID {Id}: {ex.Message}\nStack Trace: {ex.StackTrace}";
                    WriteLogToFile(LogFilePath, logMessageError);
                }
                
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
