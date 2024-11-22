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
                $"clients" => await dbContext.Client.FirstOrDefaultAsync(c => c.Id == TestParams.GetTestID),
                $"warehouses" => await dbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == TestParams.GetTestID),
                $"locations" => await dbContext.Location.FirstOrDefaultAsync(l => l.Id == TestParams.GetTestID),
                $"transfers" => await dbContext.Transfer.FirstOrDefaultAsync(t => t.Id == TestParams.GetTestID),
                $"items" => await dbContext.Item.FirstOrDefaultAsync(i => i.Id == TestParams.GetTestID),
                $"itemlines" => await dbContext.ItemLine.FirstOrDefaultAsync(il => il.Id == TestParams.GetTestID),
                $"itemgroups" => await dbContext.ItemGroup.FirstOrDefaultAsync(ig => ig.Id == TestParams.GetTestID),
                $"itemtypes" => await dbContext.ItemType.FirstOrDefaultAsync(it => it.Id == TestParams.GetTestID),
                $"inventories" => await dbContext.Inventory.FirstOrDefaultAsync(inv => inv.Id == TestParams.GetTestID),
                $"suppliers" => await dbContext.Supplier.FirstOrDefaultAsync(s => s.Id == TestParams.GetTestID),
                $"orders" => await dbContext.Order.FirstOrDefaultAsync(o => o.Id == TestParams.GetTestID),
                $"shipments" => await dbContext.Shipment.FirstOrDefaultAsync(sh => sh.Id == TestParams.GetTestID),
                _ => throw new ArgumentException($"No matching table found for endpoint: {table}")
            };
        }
}
