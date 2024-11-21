using Newtonsoft.Json;
using CargoHub.Models;

public class MigrationService
{
    private DatabaseContext _context;
    public MigrationService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task MigrateAll()
    {
        // await _context.Database.EnsureDeletedAsync();
        // await _context.Database.EnsureCreatedAsync();
        // await MigrateClients();
        // await MigrateInventories();
        // await MigrateItem();
        // await MigrateItemGroup();
        // await MigrateItemLine();
        // await MigrateItemType();
        // await MigrateLocation();
        // await MigrateOrder();
        await MigrateShipments();
        await MigrateSupplier();
        await MigrateTransfer();
        await MigrateWarehouse();
    }

    public async Task MigrateClients()
    {
        _context.Client.RemoveRange(_context.Client);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "clients.json");
        var clients = JsonConvert.DeserializeObject<List<Client>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.Client.AddRange(clients);
        _context.SaveChanges();
    }

    public async Task MigrateInventories()
    {
        _context.Inventory.RemoveRange(_context.Inventory);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "inventories.json");
        var inventories = JsonConvert.DeserializeObject<List<Inventory>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.Inventory.AddRange(inventories);
        _context.SaveChanges();
    }

    public async Task MigrateItem()
    {
        _context.Item.RemoveRange(_context.Item);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "items.json");
        var items = JsonConvert.DeserializeObject<List<Item>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.Item.AddRange(items);
        _context.SaveChanges();
    }

    public async Task MigrateItemGroup()
    {
        _context.ItemGroup.RemoveRange(_context.ItemGroup);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "item_groups.json");
        var item_groups = JsonConvert.DeserializeObject<List<ItemGroup>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.ItemGroup.AddRange(item_groups);
        _context.SaveChanges();
    }

    public async Task MigrateItemLine()
    {
        _context.ItemLine.RemoveRange(_context.ItemLine);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "item_lines.json");
        var item_lines = JsonConvert.DeserializeObject<List<ItemLine>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.ItemLine.AddRange(item_lines);
        _context.SaveChanges();
    }

    public async Task MigrateItemType()
    {
        _context.ItemType.RemoveRange(_context.ItemType);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "item_types.json");
        var item_types = JsonConvert.DeserializeObject<List<ItemType>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.ItemType.AddRange(item_types);
        _context.SaveChanges();
    }

    public async Task MigrateLocation()
    {
        _context.Location.RemoveRange(_context.Location);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "locations.json");
        var locations = JsonConvert.DeserializeObject<List<Location>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.Location.AddRange(locations);
        _context.SaveChanges();
    }

    public async Task MigrateOrder()
    {
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "orders.json");
        var orders = JsonConvert.DeserializeObject<List<Order>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        int prev = 1;
        const int batchSize = 10;
        for (int i = 0; i < orders.Count; i += batchSize)
        {
            var batch = orders.Skip(i).Take(batchSize).ToList();
            
            // Ensure ItemsJson is populated
            foreach (var order in batch)
            {
                order.ItemsJson = JsonConvert.SerializeObject(order.Items);
                if (order.Id < prev)
                {
                    order.Id = prev;
                }
                prev = prev + 1;
            }

            _context.Order.AddRange(batch);
            await _context.SaveChangesAsync();
        }
    }

    public async Task MigrateShipments()
    {
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "shipments.json");
        var shipments = JsonConvert.DeserializeObject<List<Shipment>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        const int batchSize = 1000;
        for (int i = 0; i < shipments.Count; i += batchSize)
        {
            var batch = shipments.Skip(i).Take(batchSize).ToList();
            
            // Ensure ItemsJson is populated
            foreach (var shipment in batch)
            {
                shipment.ItemsJson = JsonConvert.SerializeObject(shipment.Items);
            }

            _context.Shipment.AddRange(batch);
            await _context.SaveChangesAsync();
        }
    }


    public async Task MigrateSupplier()
    {
        _context.Supplier.RemoveRange(_context.Supplier);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "suppliers.json");
        var suppliers = JsonConvert.DeserializeObject<List<Supplier>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        _context.Supplier.AddRange(suppliers);
        _context.SaveChanges();
    }

    public async Task MigrateTransfer()
    {
        _context.Transfer.RemoveRange(_context.Transfer);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "transfers.json");
        var transfers = JsonConvert.DeserializeObject<List<Transfer>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        const int batchSize = 1000;
        for (int i = 0; i < transfers.Count; i += batchSize)
        {
            var batch = transfers.Skip(i).Take(batchSize).ToList();
            
            // Ensure ItemsJson is populated
            foreach (var transfer in batch)
            {
                transfer.ItemsJson = JsonConvert.SerializeObject(transfer.Items);
            }

            _context.Transfer.AddRange(batch);
            await _context.SaveChangesAsync();
        }
    }

    public async Task MigrateWarehouse()
    {
        _context.Warehouse.RemoveRange(_context.Warehouse);
        _context.SaveChanges();
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "jsonData", "warehouses.json");
        var warehouses = JsonConvert.DeserializeObject<List<Warehouse>>(
            await File.ReadAllTextAsync(jsonPath)
        );

        foreach (var warehouse in warehouses)
        {
            _context.Contact.Add(warehouse.Contact);
            _context.Warehouse.AddRange(warehouse);
        }
        _context.SaveChanges();
    }
}