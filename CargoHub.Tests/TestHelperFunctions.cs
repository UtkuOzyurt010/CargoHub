using CargoHub.Models;
using CargoHub.Tests;
using Microsoft.EntityFrameworkCore;
public static class TestHelperFunctions
{
    public static void WriteLogToFile(string filePath, string message)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            writer.WriteLine(message);
        }
    }
        public static async Task<object> GetDBTable(string endpoint, DatabaseContext dbContext)
        {
            return endpoint switch
            {
                $"/api/{Globals.Version}/clients" => await dbContext.Client.FirstOrDefaultAsync(c => c.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/warehouses" => await dbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/locations" => await dbContext.Location.FirstOrDefaultAsync(l => l.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/transfers" => await dbContext.Transfer.FirstOrDefaultAsync(t => t.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/items" => await dbContext.Item.FirstOrDefaultAsync(i => i.Uid == TestParams.GetItemID),
                $"/api/{Globals.Version}/itemlines" => await dbContext.ItemLine.FirstOrDefaultAsync(il => il.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/itemgroups" => await dbContext.ItemGroup.FirstOrDefaultAsync(ig => ig.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/itemtypes" => await dbContext.ItemType.FirstOrDefaultAsync(it => it.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/inventories" => await dbContext.Inventory.FirstOrDefaultAsync(inv => inv.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/suppliers" => await dbContext.Supplier.FirstOrDefaultAsync(s => s.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/orders" => await dbContext.Order.FirstOrDefaultAsync(o => o.Id == TestParams.GetTestID),
                $"/api/{Globals.Version}/shipments" => await dbContext.Shipment.FirstOrDefaultAsync(sh => sh.Id == TestParams.GetTestID),
                _ => throw new ArgumentException($"No matching table found for endpoint: {endpoint}")
            };
        }
}
