using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using CargoHub.Models;
using Xunit;
using static TestHelperFunctions;
using Microsoft.VisualStudio.TestPlatform.TestExecutor;

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

        public async Task<Client?> GetClientByIdFromDb(int clientId, bool unittest = false)
        {
            if (unittest) clientId = TestID;
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            var clientFromDb = await context.Client.FindAsync(clientId);
            // Log the database finding to a file
            bool success = clientFromDb.Id == clientId;
            string logFilePath = $"C:/CargoHub2/CargoHub/CargoHub.Tests/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string logMessage = $"Test: {success}\n" +
                                $"Database Client Name: {clientFromDb?.Name}\n" +
                                $"Database Client ID: {clientFromDb?.Id}\n\n";

            // Write log to the file
            WriteLogToFile(logFilePath, logMessage);
            return clientFromDb;
        }
    }
}