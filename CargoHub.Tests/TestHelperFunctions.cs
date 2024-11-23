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
        public static async Task<object>GetDBTable(string  table, DatabaseContext dbContext)
        {
            return table switch
            {
                $"clients" => await dbContext.Client.FirstOrDefaultAsync(c => c.Id == TestParams.TestID),
                $"warehouses" => await dbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == TestParams.TestID),
                $"locations" => await dbContext.Location.FirstOrDefaultAsync(l => l.Id == TestParams.TestID),
                $"transfers" => await dbContext.Transfer.FirstOrDefaultAsync(t => t.Id == TestParams.TestID),
                $"items" => await dbContext.Item.FirstOrDefaultAsync(i => i.Id == TestParams.TestID),
                $"itemlines" => await dbContext.ItemLine.FirstOrDefaultAsync(il => il.Id == TestParams.TestID),
                $"itemgroups" => await dbContext.ItemGroup.FirstOrDefaultAsync(ig => ig.Id == TestParams.TestID),
                $"itemtypes" => await dbContext.ItemType.FirstOrDefaultAsync(it => it.Id == TestParams.TestID),
                $"inventories" => await dbContext.Inventory.FirstOrDefaultAsync(inv => inv.Id == TestParams.TestID),
                $"suppliers" => await dbContext.Supplier.FirstOrDefaultAsync(s => s.Id == TestParams.TestID),
                $"orders" => await dbContext.Order.FirstOrDefaultAsync(o => o.Id == TestParams.TestID),
                $"shipments" => await dbContext.Shipment.FirstOrDefaultAsync(sh => sh.Id == TestParams.TestID),
                _ => throw new ArgumentException($"No matching table found for endpoint: {table}")
            };
        }
}
