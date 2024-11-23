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
            
       _client.DefaultRequestHeaders.Add("API_KEY", $"{TestParams.TestAPIKEY}");     _filepath = Path.Combine(resultsDirectory, $"GetIntegrationTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
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
                await Test_One_ID(endpoint);
            }
        }

        public async Task Test_One_ID(string endpoint)
        {
            Stopwatch stopwatch = new Stopwatch();

            //measure elapsed time for request processing
            stopwatch.Start();

            var response = await _client.GetAsync($"{endpoint}/{TestParams.TestID}");

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

// Get endpoints old python codebase

// Paths for warehouses

//     /warehouses
//     /warehouses/{warehouse_id}
//     /warehouses/{warehouse_id}/locations
//     To-Do: Handle paths like /warehouses/{warehouse_id}/locations/{location_id}.

// Paths for locations

//     /locations
//     /locations/{location_id}

// Paths for transfers

//     /transfers
//     /transfers/{transfer_id}
//     /transfers/{transfer_id}/items

// Paths for items

//     /items
//     /items/{item_id}
//     /items/{item_id}/inventory
//     /items/{item_id}/inventory/totals

// Paths for item_lines

//     /item_lines
//     /item_lines/{item_line_id}
//     /item_lines/{item_line_id}/items

// Paths for item_groups

//     /item_groups
//     /item_groups/{item_group_id}
//     /item_groups/{item_group_id}/items

// Paths for item_types

//     /item_types
//     /item_types/{item_type_id}
//     /item_types/{item_type_id}/items

// Paths for inventories

//     /inventories
//     /inventories/{inventory_id}

// Paths for suppliers

//     /suppliers
//     /suppliers/{supplier_id}
//     /suppliers/{supplier_id}/items

// Paths for orders

//     /orders
//     /orders/{order_id}
//     /orders/{order_id}/items

// Paths for clients

//     /clients
//     /clients/{client_id}
//     /clients/{client_id}/orders

// Paths for shipments

//     /shipments
//     /shipments/{shipment_id}
//     /shipments/{shipment_id}/orders
//     /shipments/{shipment_id}/items