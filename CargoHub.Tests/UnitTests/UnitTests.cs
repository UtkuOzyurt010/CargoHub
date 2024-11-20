using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using CargoHub.Models;
using static TestHelperFunctions;

namespace CargoHub.Tests
{
    public class UnitTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly int TestID = 1;

        public UnitTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        public async Task<Client?> GetByIdFromDb(int clientId, bool unittest = false)
        {
            if (unittest) clientId = TestID;
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var FromDb = await context.Client.FindAsync(clientId);
            // Log the database finding to a file
            bool success = FromDb.Id == clientId;
            string logFilePath = $"C:/CargoHub2/CargoHub/CargoHub.Tests/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string logMessage = $"Test: {success}\n" +
                                $"Database ID: {FromDb?.Id}\n\n";

            // Write log to the file
            WriteLogToFile(logFilePath, logMessage);
            return FromDb;
        }
    }
}