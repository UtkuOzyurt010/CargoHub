using CargoHub.Models;
using Newtonsoft.Json;

namespace CargoHub.Tests
{
    public static class TestParams
    {
        public static int TestID = 1;
        public static int PPDTestID = 999999;

        public static string TestAPIKEY = "a1b2c3d4e5";

        public static Warehouse Warehousedummydata = new Warehouse
        {
            Id = PPDTestID,
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

        public static Location Locationdummydata = new Location
        {
            Id = PPDTestID,
            Warehouse_Id = 999,
            Code = "LOC-001",
            Name = "Main Storage",
            Created_At = DateTime.Parse("2024-01-01T10:00:00Z"),
            Updated_At = DateTime.Parse("2024-11-01T15:30:00Z")
        };

        public static Transfer Transferdummydata = new Transfer
        {
            Id = PPDTestID,
            Reference = "TRF-20231123",
            Transfer_From = 1001,
            Transfer_To = 2002,
            Transfer_Status = "Pending",
            Created_At = DateTime.Parse("2024-01-01T10:00:00Z"),
            Updated_At = DateTime.Parse("2024-11-01T15:30:00Z"), 
            ItemsJson = JsonConvert.SerializeObject(new List<TransferItem>
            {
                new TransferItem { Item_Id = "101", Amount = 50 },
                new TransferItem { Item_Id = "102", Amount = 20 }
            })
        };

        public static Item Itemdummydata = new Item
        {
            Id = PPDTestID,
            Uid = "P011721",
            Code = "ITEM-001",
            Description = "A sample item for testing purposes.",
            Short_Description = "Sample Item",
            Upc_Code = "123456789012",
            Model_Number = "MODEL-001",
            Commodity_Code = "COM-001",
            Item_Line = 10,
            Item_Group = 5,
            Item_Type = 3,
            Unit_Purchase_Quantity = 100,
            Unit_Order_Quantity = 50,
            Pack_Order_Quantity = 10,
            Supplier_Id = 2001,
            Supplier_Code = "SUP-001",
            Supplier_Part_Number = "PART-001",
            Created_At = DateTime.UtcNow,
            Updated_At = DateTime.UtcNow
        };

        public static Inventory Inventorydummydata = new Inventory
        {
            Id = PPDTestID,
            Item_Id = "ITEM-001",
            Description = "Sample inventory record for an item.",
            Item_Reference = "REF-12345",
            Locations = new List<int> { 101, 102, 103 },
            Total_On_Hand = 150,
            Total_Expected = 50,
            Total_Ordered = 100,
            Total_Allocated = 30,
            Total_Available = 120,
            Created_At = DateTime.UtcNow,
            Updated_At = DateTime.UtcNow
        };

        public static Supplier Supplierdummydata = new Supplier
        {
            Id = PPDTestID,
            Code = "SUP-001",
            Name = "Global Supply Co.",
            Address = "123 Supply Lane",
            Address_Extra = "Building B, Suite 5",
            City = "Metropolis",
            Zip_Code = "54321",
            Province = "CA",
            Country = "USA",
            Contact_Name = "Jane Smith",
            Phonenumber = "+1-555-678-9012",
            Reference = "GS-REF-2024",
            Created_At = DateTime.UtcNow.AddMonths(-6),
            Updated_At = DateTime.UtcNow
        };

        public static Order Orderdummydata = new Order
        {
            Id = PPDTestID,
            Source_Id = 101,
            Order_Date = DateTime.UtcNow.AddDays(-7),
            Request_Date = DateTime.UtcNow.AddDays(7),
            Reference = "ORD-2024-001",
            Reference_Extra = "Extra reference info",
            Order_Status = "Pending",
            Notes = "Urgent delivery requested.",
            Shipping_Notes = "Deliver to rear entrance.",
            Picking_Notes = "Fragile items, handle with care.",
            Warehouse_Id = 2001,
            Ship_To = 3001,
            Bill_To = 3002,
            Shipment_Id = 4001,
            Total_Amount = 2500.75m,
            Total_Discount = 150.25m,
            Total_Tax = 250.00m,
            Total_Surcharge = 50.00m,
            Created_At = DateTime.UtcNow.AddMonths(-1),
            Updated_At = DateTime.UtcNow,
            ItemsJson = JsonConvert.SerializeObject(new List<OrderItem>
            {
                new OrderItem { Item_Id = "ITEM001", Amount = 10 },
                new OrderItem { Item_Id = "ITEM002", Amount = 5 }
            })
        };

        public static Client Clientdummydata = new Client
        {
            Id = PPDTestID,
            Name = "Acme Corporation",
            Address = "123 Main Street",
            City = "Metropolis",
            zip_code = "12345",
            Province = "Central State",
            Country = "Fictionland",
            contact_name = "John Doe",
            contact_email = "johndoe@acme.com",
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
            UpdatedAt = DateTime.UtcNow
        };

        public static Shipment Shipmentdummydata = new Shipment
        {
            Id = PPDTestID,
            Order_Id = 101,
            Source_Id = 501,
            Order_Date = DateTime.UtcNow.AddDays(-10),
            Request_Date = DateTime.UtcNow.AddDays(-7),
            Shipment_Date = DateTime.UtcNow.AddDays(-3),
            Shipment_Type = "Express",
            Shipment_Status = "In Transit",
            Notes = "Handle with care. Fragile items included.",
            Carrier_Code = "DHL",
            Carrier_Description = "DHL Express Shipping",
            Service_Code = "EXP123",
            Payment_Type = "Prepaid",
            Transfer_Mode = "Air",
            Total_Package_Count = 5,
            Total_Package_Weight = 120.75M,
            Created_At = DateTime.UtcNow.AddDays(-10),
            Updated_At = DateTime.UtcNow,

            ItemsJson = JsonConvert.SerializeObject(new List<ShipmentItem>
            {
                new ShipmentItem { Item_Id = "ITM001", Amount = 3 },
                new ShipmentItem { Item_Id = "ITM002", Amount = 2 },
            })
        };

    }
}