using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using CargoHub.Models;
using static TestHelperFunctions;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Tests
{
    public class UnitTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly string _filepath;

        public UnitTests(WebApplicationFactory<Program> factory)
        {
            var projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            var resultsDirectory = Path.Combine(projectRoot, "..", "..", "..", "CargoHub.Tests", "UnitTests", "Test_Results");
            Directory.CreateDirectory(resultsDirectory); 
            _factory = factory;
            _filepath = Path.Combine(resultsDirectory, $"UnitTests - {DateTime.Now.ToString("dd-MM-yyyy-HH-mm")}.txt");
        }

        [Fact]
        public async Task Test_Get_Id_FromDB()
        {
            var Tables = new List<string>
            {
                "clients",
                "warehouses",
                "locations",
                "transfers",
                "items",
                "itemlines",
                "itemgroups",
                "itemtypes",
                "inventories",
                "suppliers",
                "orders",
                "shipments",
            };

            foreach (var table in Tables)
            {
                await GetFromDb(table);
            }
        }

        public async Task GetFromDb(string table)
        {
            //stopwatch for keks
            Stopwatch stopwatch = new Stopwatch();
            //get access to  test database
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

            stopwatch.Start();

            var FromDb = await GetDBTable(table, TestParams.TestID, dbContext);

            stopwatch.Stop();

            // Log the database finding to a file
            Assert.NotNull(FromDb);

            var message = $"Test: GetfromDB\nTestID: {TestParams.TestID}\n" +
                          $"DBID: {FromDb}\n" +
                          $"Test executed in: {stopwatch.ElapsedMilliseconds}ms\n\n";

            // Write log to the file
            WriteLogToFile(_filepath, message);
        }
    }
}