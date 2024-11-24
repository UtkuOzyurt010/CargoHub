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
    public static async Task<object> GetDBTable(string  table, int id,DatabaseContext dbContext)
    {
        return table switch
        {
            $"clients" => await dbContext.Client.FirstOrDefaultAsync(c => c.Id == id),
            $"warehouses" => await dbContext.Warehouse.FirstOrDefaultAsync(w => w.Id == id),
            $"locations" => await dbContext.Location.FirstOrDefaultAsync(l => l.Id == id),
            $"transfers" => await dbContext.Transfer.FirstOrDefaultAsync(t => t.Id == id),
            $"items" => await dbContext.Item.FirstOrDefaultAsync(i => i.Id == id),
            $"itemlines" => await dbContext.ItemLine.FirstOrDefaultAsync(il => il.Id == id),
            $"itemgroups" => await dbContext.ItemGroup.FirstOrDefaultAsync(ig => ig.Id == id),
            $"itemtypes" => await dbContext.ItemType.FirstOrDefaultAsync(it => it.Id == id),
            $"inventories" => await dbContext.Inventory.FirstOrDefaultAsync(inv => inv.Id == id),
            $"suppliers" => await dbContext.Supplier.FirstOrDefaultAsync(s => s.Id == id),
            $"orders" => await dbContext.Order.FirstOrDefaultAsync(o => o.Id == id),
            $"shipments" => await dbContext.Shipment.FirstOrDefaultAsync(sh => sh.Id == id),
            _ => throw new ArgumentException($"No matching table found for endpoint: {table}")
        };
    }
        
    public static object GetDummyData(string endpoint) => endpoint switch
    {
        "warehouses" => TestParams.Warehousedummydata,
        "shipments" => TestParams.Shipmentdummydata,
        "clients" => TestParams.Clientdummydata,
        "suppliers" => TestParams.Supplierdummydata,
        "orders" => TestParams.Orderdummydata,
        "inventories" => TestParams.Inventorydummydata,
        "locations" => TestParams.Locationdummydata,
        "transfers" => TestParams.Transferdummydata,
        "items" => TestParams.Itemdummydata,
        _ => throw new ArgumentException($"Invalid data type: {endpoint}")
    };
}
