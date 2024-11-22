using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using CargoHub.Models;
using static TestHelperFunctions;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


namespace CargoHub.Tests
{
    public class GetIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _filepath;

        public GetIntegrationTests(WebApplicationFactory<Program> factory)
        {
            var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            var resultsDirectory = Path.Combine(projectRoot, "..", "..", "..", "CargoHub.Tests", "GetIntegrationTests", "Test_Results");
            Directory.CreateDirectory(resultsDirectory); 
            _factory = factory;
            _client = factory.CreateClient();
            _filepath = Path.Combine(resultsDirectory, $"GetIntegrationTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
        }

        [Fact]
        public async Task Test_Get_Id_Endpoints()
        {
            var endpointsWithIds = new List<string>
            {
                $"/api/{Globals.Version}/clients",
                $"/api/{Globals.Version}/warehouses",
                $"/api/{Globals.Version}/locations",
                $"/api/{Globals.Version}/transfers",
                $"/api/{Globals.Version}/items",
                $"/api/{Globals.Version}/itemlines",
                $"/api/{Globals.Version}/itemgroups",
                $"/api/{Globals.Version}/itemtypes",
                $"/api/{Globals.Version}/inventories",
                $"/api/{Globals.Version}/suppliers",
                $"/api/{Globals.Version}/orders",
                $"/api/{Globals.Version}/shipments",
            };

            foreach (var endpoint in endpointsWithIds)
            {
                await Test_One_ID(endpoint, TestParams.GetTestID);
            }
        }

        public async Task Test_One_ID(string endpoint, int TestID)
        {
            Stopwatch stopwatch = new Stopwatch();

            //measure elapsed time for request processing
            stopwatch.Start();
            HttpResponseMessage response = default;
            if (endpoint == $"/api/{Globals.Version}/items")
            {
                response = await _client.GetAsync($"{endpoint}/{TestParams.GetItemID}");
            } 
            else 
            {
                response = await _client.GetAsync($"{endpoint}/{TestParams.GetTestID}");
            }
            stopwatch.Stop();

            var responseBody = await response.Content.ReadAsStringAsync();

            //Assert server returns OK and response contains correct info
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            string ID = default;

            //get access to de database
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            if (endpoint == $"/api/{Globals.Version}/items")
            {
                var Itementity = (Item)await GetDBTable(endpoint, dbContext);
                Assert.NotNull(Itementity);
                //Assert.Equal(ItemID, Itementity.Uid);
                ID = TestParams.GetItemID;
            } 
            else 
            {
                var dbentity = await GetDBTable(endpoint, dbContext);
                Assert.NotNull(dbentity);
                //Assert.Equal(TestID, dbentity.Id);
                ID = TestParams.GetTestID.ToString();
            }
            
            
            var message = $"Test: Get_ById_ReturnsDetails\nStatusCode: {response.StatusCode}\n" +
                          $"Response: {responseBody}\nEndpoint: {endpoint}/{ID}\n" +
                          $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n\n";

            //logging info
            WriteLogToFile(_filepath, message);
        }
    }
}
