using CargoHub.Models;

namespace CargoHub.Tests
{
    public static class TestParams
    {
        public static int TestID = 1;

        public static Warehouse WarehousedummyData = new Warehouse
        {
            Id = 1,
            Code = "WH-001",
            Name = "Central Warehouse",
            Address = "123 Main Street",
            Zip = "12345",
            City = "Springfield",
            Province = "IL",
            Country = "USA",
            Contact = new Contact
            {
                Name = "John Doe",
                Phone = "+1-555-123-4567",
                Email = "john.doe@example.com"
            },
            Created_At = DateTime.Parse("2024-01-01T10:00:00Z"),
            Updated_At = DateTime.Parse("2024-11-01T15:30:00Z")
        };

    }
}