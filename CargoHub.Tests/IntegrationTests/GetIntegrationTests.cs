using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Runtime.CompilerServices;
using static TestHelperFunctions;


namespace CargoHub.Tests
{
    public class GetIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _filepath;
        private readonly int TestID = 1;
        private readonly string ItemID = "P000001";

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
                $"/api/v1/clients",
                $"/api/v1/warehouses",
                $"/api/v1/locations",
                $"/api/v1/transfers",
                $"/api/v1/items",
                $"/api/v1/itemlines",
                $"/api/v1/itemgroups",
                $"/api/v1/itemtypes",
                $"/api/v1/inventories",
                $"/api/v1/suppliers",
                $"/api/v1/orders",
                $"/api/v1/shipments",
            };

            foreach (var endpoint in endpointsWithIds)
            {
                await Test_One_ID(endpoint, TestID);
            }
        }

        public async Task Test_One_ID(string endpoint, int TestID)
        {
            HttpResponseMessage response = default;
            if (endpoint == "/api/v1/items")
            {
                response = await _client.GetAsync($"{endpoint}/{ItemID}");
            } 
            else 
            {
                response = await _client.GetAsync($"{endpoint}/{TestID}");
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var message = $"Test: Get_ById_ReturnsDetails\nStatusCode: {response.StatusCode}\nResponse: {responseBody}\nEndpoint: {endpoint}/{TestID}\n";
            WriteLogToFile(_filepath, message);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            

        }
    }
}
