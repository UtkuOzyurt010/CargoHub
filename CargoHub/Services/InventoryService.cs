using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class InventoryService : IGenericService<Inventory>
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

        public async Task<List<Inventory>> GetBatch(List<int> ids)
        {
            List<Inventory> result = await _context.Inventory.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
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
            if(inventory is not null)
            {
                var register = await _context.Inventory.FindAsync(inventory.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Inventory.AddAsync(inventory);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
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
            if(DBinventory is not null)
            {
                DBinventory = inventory;
                _context.SaveChanges();

                return true;
            }
            else return false;
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
            if(DBinventory is not null)
            {
                _context.Remove(DBinventory);
                _context.SaveChanges();
                return true;
            }
            else return false;
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
