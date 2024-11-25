using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using CargoHub.Models;
using static TestHelperFunctions;
using System.Diagnostics;
using System.Text.Json;
using System.Text;

namespace CargoHub.Tests
{
    public class PutIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _filepath;

        public PutIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            var resultsDirectory = Path.Combine(projectRoot, "..", "..", "..", "CargoHub.Tests", "PutIntegrationTests", "Test_Results");
            Directory.CreateDirectory(resultsDirectory); 
            _factory = factory;
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("API_KEY", $"{TestParams.TestAPIKEY}");
            _filepath = Path.Combine(resultsDirectory, $"PutIntegrationTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
        }

        [Fact]
        public async Task Test_Put_Id_Endpoints()
        {
            var endpointsWithIds = new List<string>
            {
                $"/api/{Globals.Version}/warehouses",
                $"/api/{Globals.Version}/locations",
                $"/api/{Globals.Version}/transfers",
                $"/api/{Globals.Version}/items",
                $"/api/{Globals.Version}/inventories",
                $"/api/{Globals.Version}/suppliers",
                $"/api/{Globals.Version}/orders",
                $"/api/{Globals.Version}/clients",
                $"/api/{Globals.Version}/shipments",
            };

            foreach (var endpoint in endpointsWithIds)
            {
                await Put_One_ID(endpoint);
                break;
            }
        }

        public async Task Put_One_ID(string endpoint)
        {
            Stopwatch stopwatch = new Stopwatch();

            //get dummy data
            var Dummydata = GetDummyData(endpoint.Split("/").Last(), true);

            // Create StringContent with JSON payload per endpoint
            var content = new StringContent(JsonSerializer.Serialize(Dummydata),
                                            Encoding.UTF8, "application/json");


            //measure elapsed time for request processing
            stopwatch.Start();

            var response = await _client.PutAsync($"{endpoint}/{TestParams.PPDTestID}", content);

            stopwatch.Stop();

            //Assert server returns OK and response contains correct info
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //get access to test database
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            var dbentity = await GetDBTable(endpoint.Split("/").Last(), TestParams.PPDTestID, dbContext);

            Assert.NotNull(dbentity);
            Assert.Equal(TestParams.PPDTestID, dbentity.Id);
            
            var message = $"Test: Put_ReturnsDetails\nStatusCode: {response.StatusCode}\n" +
                          $"Endpoint: {endpoint}\n" +
                          $"DB: {dbentity.Id}\n" +
                          $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n\n";

            //logging info
            WriteLogToFile(_filepath, message);
        }
    }
}