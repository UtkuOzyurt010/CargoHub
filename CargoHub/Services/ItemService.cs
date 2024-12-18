using CargoHub.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CargoHub.Services{
    public class ItemService : IItemService
    {
        private DatabaseContext _context;

        public ItemService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Item?> Get(object identifier)
        {
            Item? item = identifier switch
            {
                int id => await _context.Item.FindAsync(id), // Use Id if it's an integer
                string uid => await _context.Item.FirstOrDefaultAsync(i => i.Uid == uid), // Use Uid if it's a string
                _ => null // Return null if it's neither an int nor a string
            };

            return item;
        }

        public async Task<List<Item>> GetSupplierItems(int id)
        {
            var result = await _context.Item
                         .Where(supplier => supplier.Supplier_Id == id)
                         .ToListAsync();
            return result;
        }

        public List<OrderItem> GetOrderItems(string itemsJson)
        {
            var orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(itemsJson);
            return orderItems;
        }

        public List<ShipmentItem> GetShipmentItems(string itemsJson)
        {
            var shipmentItems = JsonConvert.DeserializeObject<List<ShipmentItem>>(itemsJson);
            return shipmentItems;
        }

        public List<TransferItem> GetTransferItems(string itemsJson)
        {
            var transferItems = JsonConvert.DeserializeObject<List<TransferItem>>(itemsJson);
            return transferItems;
        }

        public async Task<List<Item>> GetItemGroupItems(int id)
        {
            var result = await _context.Item
            .Where(x => x.Item_Group == id)
            .ToListAsync();
            return result;
        }

        public async Task<List<Item>> GetItemLineItems(int id)
        {
            var result = await _context.Item
            .Where(x => x.Item_Line == id)
            .ToListAsync();
            return result;
        }

        public async Task<List<Item>> GetItemTypeItems(int id)
        {
            var result = await _context.Item
            .Where(x => x.Item_Type == id)
            .ToListAsync();
            return result;
        }
        
        public async Task<List<Item>> GetBatch(List<string> uids)
        {
            List<Item> result = await _context.Item.
                                        Where(x=>uids.Contains(x.Uid)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Item>> GetAll()
        {
            var query = _context.Item.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Item> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Item item)
        {
            if(item is null) return false;

            var register = await _context.Item.FindAsync(item.Id);

            if(register is not null) return false;

            await _context.Item.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> PostBatch(List<Item> items)
        {
            var results = new List<bool> {};
            foreach (Item item in items)
            {
                bool result = await Post(item);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Item item)
        {
            var DBitem = await _context.Item.FindAsync(item.Id);

            if(DBitem is null) return false;

            DBitem.Uid = item.Uid;
            DBitem.Code = item.Code;
            DBitem.Description = item.Description;
            DBitem.Short_Description = item.Short_Description;
            DBitem.Upc_Code = item.Upc_Code;
            DBitem.Model_Number = item.Model_Number;
            DBitem.Commodity_Code = item.Commodity_Code;
            DBitem.Item_Line = item.Item_Line;
            DBitem.Item_Group = item.Item_Group;
            DBitem.Item_Type = item.Item_Type;
            DBitem.Unit_Purchase_Quantity = item.Unit_Purchase_Quantity;
            DBitem.Unit_Order_Quantity = item.Unit_Order_Quantity;
            DBitem.Pack_Order_Quantity = item.Pack_Order_Quantity;
            DBitem.Supplier_Id = item.Supplier_Id;
            DBitem.Supplier_Code = item.Supplier_Code;
            DBitem.Supplier_Part_Number = item.Supplier_Part_Number;
            DBitem.Updated_At = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> UpdateBatch(List<Item> items)
        {
            var results = new List<bool> {};
            foreach (Item item in items)
            {
                bool result = await Update(item);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(object identifier)
        {
            Item? DBitem = identifier switch
            {
                int id => await _context.Item.FindAsync(id), // Use Id if it's an integer
                string uid => await _context.Item.FirstOrDefaultAsync(i => i.Uid == uid), // Use Uid if it's a string
                _ => null // Return null if it's neither an int nor a string
            };

            if(DBitem is null) return false;

            _context.Remove(DBitem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> DeleteBatch(List<string> uids)
        {
            var results = new List<bool> {};
            foreach (string uid in uids)
            {
                bool result = await Delete(uid);
                results.Add(result);
            }
            return results;
        }
    }
}
