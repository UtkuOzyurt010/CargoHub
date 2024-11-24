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

        public async Task<List<OrderItem>> GetOrderItems(string itemsJson)
        {
            var orderItems = JsonConvert.DeserializeObject<List<OrderItem>>(itemsJson);
            return orderItems;
        }

        public async Task<List<ShipmentItem>> GetShipmentItems(string itemsJson)
        {
            var shipmentItems = JsonConvert.DeserializeObject<List<ShipmentItem>>(itemsJson);
            return shipmentItems;
        }

        public async Task<List<TransferItem>> GetTransferItems(string itemsJson)
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
            if(item is not null)
            {
                var register = await _context.Item.FirstOrDefaultAsync(x => x.Uid == item.Uid);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Item.AddAsync(item);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
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
            var DBitem = await _context.Item.FindAsync(item.Uid);
            if(DBitem is not null)
            {
                DBitem = item;
                _context.SaveChanges();

                return true;
            }
            else return false;
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

        public async Task<bool> Delete(string uid)
        {
            var DBitem = await _context.Item.FindAsync(uid);
            if(DBitem is not null)
            {
                _context.Remove(DBitem);
                _context.SaveChanges();
                return true;
            }
            else return false;
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
