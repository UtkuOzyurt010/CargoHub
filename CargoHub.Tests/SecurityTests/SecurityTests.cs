using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using CargoHub.Models;
using static TestHelperFunctions;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CargoHub.Tests
{
    public class AuthTests : IClassFixture<WebApplicationFactory<Program>>
    {
        
        private readonly HttpClient _client;
        private readonly string _filepath;

        public AuthTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            var resultsDirectory = Path.Combine(projectRoot, "..", "..", "..", "CargoHub.Tests", "SecurityTests", "Test_Results");
            Directory.CreateDirectory(resultsDirectory);
            _filepath = Path.Combine(resultsDirectory, $"SecurityTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
        }

        [Fact]
        public async Task UnauthorizedReturn401()
        {
            // Data
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
            
            // Act
            foreach (var endpoint in endpoints)
            {
                var response = await _client.GetAsync(endpoint);
                Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

                var message = $"Test: Security401\nStatusCode: {response.StatusCode}\n" +
                          $"Endpoint: {endpoint}\n";
                WriteLogToFile(_filepath, message);
            }
        }

        [Fact]
        public async Task FalseAPIKeyReturn403()
        {
            // Arrange
             _client.DefaultRequestHeaders.Add("API_KEY", "DefinitelyWrongKey");

             // Data
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

            // Forbidden
            foreach (var endpoint in endpoints)
            {
                var response = await _client.GetAsync(endpoint);
                Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
                var message = $"Test: Security403\nStatusCode: {response.StatusCode}\n" +
                          $"Endpoint: {endpoint}\n";
                WriteLogToFile(_filepath, message);
            }
        }
    }
}