using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using CargoHub.Models;
using static TestHelperFunctions;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Tests
{
    public class UnitTests : DbContext
    {
        private readonly DatabaseContext _context;
        private readonly int TestID = 1;

        public UnitTests(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByIdFromDb(int clientId, bool unittest = false)
        {
            if (unittest) clientId = TestID;
            var FromDb = await _context.Client.FindAsync(clientId);
            // Log the database finding to a file
            bool success = FromDb.Id == clientId;
            //string logFilePath = Path.GetFullPath("CargoHub.Tests") + $"/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            
            string logFilePath = $"C:/VSCodeProjects/CargoHub/CargoHub.Tests/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            //string logFilePath = $"/CargoHub/CargoHub.Tests/UnitTests/Test_Results/test_results{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string logMessage = $"Test: {success}\n" +
                                $"Database ID: {FromDb?.Id}\n\n";

            // Write log to the file
            WriteLogToFile(logFilePath, logMessage);
            return FromDb;
        }
    }
}