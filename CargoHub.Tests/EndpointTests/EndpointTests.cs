using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Testing;
using static TestHelperFunctions;
using CargoHub.Models;
using CargoHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using SQLitePCL;
using Microsoft.AspNetCore.Hosting;

namespace CargoHub.Tests
{
    public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly DatabaseContext _context;
        private readonly int TestId = 1;

        private readonly string Address = $"http://localhost:8000";

        public EndpointTests(WebApplicationFactory<Program> factory)
        {
            // Set up a custom WebApplicationFactory to mock the database
            _factory = factory;

            // Create the client to interact with the app
            _client = factory.CreateClient();
            var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            _context = context;

        }

        [Fact]
        public async Task Test_All_Id_Endpoints()
        {
            var endpointsWithIds = new List<string>
            {
                $"{Address}/api/v1/clients",
                $"{Address}/api/v1/warehouses",
                $"{Address}/api/v1/locations",
                $"{Address}/api/v1/transfers",
                $"{Address}/api/v1/items",
                $"{Address}/api/v1/item_lines",
                $"{Address}/api/v1/item_groups",
                $"{Address}/api/v1/item_types",
                $"{Address}/api/v1/inventories",
                $"{Address}/api/v1/suppliers",
                $"{Address}/api/v1/orders",
                $"{Address}/api/v1/shipments",
            };


                _context.Database.EnsureCreated();
                var client = await _context.Warehouse.FirstOrDefaultAsync(c => c.Id == 1);
                WriteLogToFile($"C:/CargoHub2/CargoHub/CargoHub.Tests/EndpointTests/Test_Results/Endpoint - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt", client.Id.ToString());
                foreach (var endpoint in endpointsWithIds)
                {
                    await Get_One_ById(endpoint, TestId);
                }
        }

        public async Task Get_One_ById(string endpoint, int Id)
        {
            //{endpoint.Split("/").Last()}-
            string LogFilePath = $"C:/CargoHub2/CargoHub/CargoHub.Tests/EndpointTests/Test_Results/Endpoint - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt";
            //string logFilePath = Path.GetFullPath("CargoHub.Tests") + $"/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            //string LogFilePath = $"C:/VSCodeProjects/CargoHub/CargoHub.Tests/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            //string LogFilePath = $"/CargoHub/CargoHub.Tests/EndpointTests/Test_Results/Endpoint - {DateTime.Now}.txt";
            string fullendpoint = $"{endpoint}/{Id}";

            var stopwatch = new Stopwatch();

            // Create instance of UnitTest to use its method
            var unitTests = new UnitTests(_context);

            stopwatch.Start();

            
            // Getresponse from endpoint
            var response = await _client.GetAsync(fullendpoint);
        
            stopwatch.Stop();

            // Assert portion
            try
            {
                if (response != null && response.StatusCode != HttpStatusCode.OK)
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
