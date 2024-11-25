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
    public class DeleteIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _filepath;

        public DeleteIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            var resultsDirectory = Path.Combine(projectRoot, "..", "..", "..", "CargoHub.Tests", "DeleteIntegrationTests", "Test_Results");
            Directory.CreateDirectory(resultsDirectory); 
            _factory = factory;
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Add("API_KEY", $"{TestParams.TestAPIKEY}");
            _filepath = Path.Combine(resultsDirectory, $"DeleteIntegrationTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
        }

        [Fact]
        public async Task Test_Delete_Id_Endpoints()
        {
            var endpoints = new List<string>
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

            foreach (var endpoint in endpoints)
            {
                await Delete_One_ID(endpoint);
            }
        }

        public async Task Delete_One_ID(string endpoint)
        {
            Stopwatch stopwatch = new Stopwatch();

            //confirm entity's existence
            var table = endpoint.Split('/').Last();
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var FromDb = await GetDBTable(table, TestParams.PPDTestID, dbContext);

            var Dummydata = GetDummyData(endpoint.Split("/").Last());

            Assert.Equal(FromDb.Id, Dummydata.Id);

            //measure elapsed time for request processing
            stopwatch.Start();

            var response = await _client.DeleteAsync($"{endpoint}/{TestParams.PPDTestID}");

            stopwatch.Stop();

            //Assert server returns OK and response contains correct info
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //get access to test database

            var dbentity = await GetDBTable(endpoint.Split("/").Last(), TestParams.PPDTestID, dbContext);

            Assert.Null(dbentity);
            //Assert.Equal(TestParams.PPDTestID, castedEntity.Id);
            
            var message = $"Test: Post_DeleteDetails\nStatusCode: {response.StatusCode}\n" +
                          $"Endpoint: {endpoint}\n" +
                          $"DB: {dbentity.Id}\n" +
                          $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n\n";

            //logging info
            WriteLogToFile(_filepath, message);
        }
    }
}