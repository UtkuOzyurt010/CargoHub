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
    public static async Task<dynamic> GetDBTable(string  table, int id,DatabaseContext dbContext)
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
        
    public static dynamic GetDummyData(string endpoint, bool modify = false) => endpoint switch
    {
        "warehouses" => modify 
            ? ModifyAndReturn(TestParams.Warehousedummydata, data => data.Code = "Modified")
            : TestParams.Warehousedummydata,
        "shipments" => modify 
            ? ModifyAndReturn(TestParams.Shipmentdummydata, data => data.Carrier_Code = "Modified")
            : TestParams.Shipmentdummydata,
        "clients" => modify 
            ? ModifyAndReturn(TestParams.Clientdummydata, data => data.Name = "Modified")
            : TestParams.Clientdummydata,
        "suppliers" => modify 
            ? ModifyAndReturn(TestParams.Supplierdummydata, data => data.Name = "Modified")
            : TestParams.Supplierdummydata,
        "orders" => modify 
            ? ModifyAndReturn(TestParams.Orderdummydata, data => data.Reference = "Modified")
            : TestParams.Orderdummydata,
        "inventories" => modify 
            ? ModifyAndReturn(TestParams.Inventorydummydata, data => data.Item_Reference = "Modified")
            : TestParams.Inventorydummydata,
        "locations" => modify 
            ? ModifyAndReturn(TestParams.Locationdummydata, data => data.Name = "Modified")
            : TestParams.Locationdummydata,
        "transfers" => modify 
            ? ModifyAndReturn(TestParams.Transferdummydata, data => data.Reference = "Modified")
            : TestParams.Transferdummydata,
        "items" => modify 
            ? ModifyAndReturn(TestParams.Itemdummydata, data => data.Code = "Modified")
            : TestParams.Itemdummydata,
        _ => throw new ArgumentException($"Invalid data type: {endpoint}")
    };

    private static T ModifyAndReturn<T>(T obj, Action<T> modifyAction)
    {
        modifyAction(obj);
        return obj;
    }
}
