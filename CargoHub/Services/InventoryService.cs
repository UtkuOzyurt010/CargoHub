using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class InventoryService : IInventoryService
    {
        private DatabaseContext _context;

        public InventoryService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Inventory?> Get(int id)
        {
            Inventory? register = await _context.Inventory.FindAsync(id);
            return register; 
        }

        public async Task<List<Inventory>> GetItemInventory(string uid)
        {
            var result = await _context.Inventory.Where(x => x.Item_Id == uid).ToListAsync();
            return result;
        }

         public async Task<dynamic> GetItemInventoryTotals(string uid)
        {
            var result = await _context.Inventory
                                    .Where(x => x.Item_Id == uid)
                                    .Select(order => new
                                    {
                                        order.Total_Expected,
                                        order.Total_Ordered,
                                        order.Total_Allocated,
                                        order.Total_Available
                                    })
                                    .FirstOrDefaultAsync();
            return result;
        }


        public async Task<List<Inventory>> GetBatch(List<int> ids)
        {
            List<Inventory> result = await _context.Inventory
                                    .Where(x=>ids.Contains(x.Id))
                                    .ToListAsync();
            return result;
        }

        public async Task<List<Inventory>> GetAll()
        {
            var query = _context.Inventory.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Inventory> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Inventory inventory)
        {
            if(inventory is null) return false;
        
            var register = await _context.Inventory.FindAsync(inventory.Id);

            if(register is not null) return false;

            await _context.Inventory.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<bool>> PostBatch(List<Inventory> inventories)
        {
            var results = new List<bool> {};
            foreach (Inventory inventory in inventories)
            {
                bool result = await Post(inventory);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Inventory inventory)
        {
            var DBinventory = await _context.Inventory.FindAsync(inventory.Id);

            if(DBinventory is null) return false;

            DBinventory.Item_Id = inventory.Item_Id;
            DBinventory.Description = inventory.Description;
            DBinventory.Item_Reference = inventory.Item_Reference;
            DBinventory.Locations = inventory.Locations;
            DBinventory.Total_On_Hand = inventory.Total_On_Hand;
            DBinventory.Total_Expected = inventory.Total_Expected;
            DBinventory.Total_Ordered = inventory.Total_Ordered;
            DBinventory.Total_Allocated = inventory.Total_Allocated;
            DBinventory.Total_Available = inventory.Total_Available;
            DBinventory.Updated_At = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> UpdateBatch(List<Inventory> inventories)
        {
            var results = new List<bool> {};
            foreach (Inventory inventory in inventories)
            {
                bool result = await Update(inventory);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBinventory = await _context.Inventory.FindAsync(id);
            if(DBinventory is null) return false;

            _context.Remove(DBinventory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> DeleteBatch(List<int> ids)
        {
            var results = new List<bool> {};
            foreach (int id in ids)
            {
                bool result = await Delete(id);
                results.Add(result);
            }
            return results;
        }
    }
}
