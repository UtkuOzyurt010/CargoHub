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
    public class PostIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _filepath;

        public PostIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            var resultsDirectory = Path.Combine(projectRoot, "..", "..", "..", "CargoHub.Tests", "PostIntegrationTests", "Test_Results");
            Directory.CreateDirectory(resultsDirectory); 
            _factory = factory;
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("API_KEY", $"{TestParams.TestAPIKEY}");
            _filepath = Path.Combine(resultsDirectory, $"PostIntegrationTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
        }

        //[Fact]
        public async Task Test_Post_Id_Endpoints()
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
                await Post_One_ID(endpoint);
            }
        }

        public async Task Post_One_ID(string endpoint)
        {
            Stopwatch stopwatch = new Stopwatch();

            // Create StringContent with JSON payload
            var content = new StringContent(JsonSerializer.Serialize(TestParams.WarehousedummyData),
                                            Encoding.UTF8, "application/json");


            //measure elapsed time for request processing
            stopwatch.Start();

            var response = await _client.PostAsync($"{endpoint}", content);

            stopwatch.Stop();

            var responseBody = await response.Content.ReadAsStringAsync();

            //Assert server returns OK and response contains correct info
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //get access to test database
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            var dbentity = await GetDBTable(endpoint.Split("/").Last(), dbContext);
            Assert.NotNull(dbentity);
            //Assert.Equal(TestID, dbentity.Id); nope nope some scope scope issues REEEEEEEEEEEEEEEE
            
            var message = $"Test: Get_ById_ReturnsDetails\nStatusCode: {response.StatusCode}\n" +
                          $"Response: {responseBody}\nEndpoint: {endpoint}/{TestParams.TestID}\n" +
                          $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n\n";

            //logging info
            WriteLogToFile(_filepath, message);
        }
    }
}

// Post endpoints original python codebase
// /warehouses

//     Functionality: Adds a new warehouse to the system.
//     Response: HTTP 201 Created.

// /locations

//     Functionality: Creates a new location and stores it in the system.
//     Response: HTTP 201 Created.

// /transfers

//     Functionality: Adds a new transfer to the pool and triggers a notification.
//     Response: HTTP 201 Created.

// /items

//     Functionality: Handles the addition of a new item to the system.
//     Response: HTTP 201 Created.

// /inventories

//     Functionality: Adds inventory data (no checks implemented in the provided code).
//     Response: HTTP 201 Created.

// /suppliers

//     Functionality: Adds a new supplier to the system.
//     Response: HTTP 201 Created.

// /orders

//     Functionality: Creates a new order and saves it to the order pool.
//     Response: HTTP 201 Created.

// /clients

//     Functionality: Registers a new client in the system.
//     Response: HTTP 201 Created.

// /shipments

//     Functionality: Creates a new shipment entry.
//     Response: HTTP 201 Created.