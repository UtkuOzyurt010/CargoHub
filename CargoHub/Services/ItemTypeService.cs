using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class ItemTypeService : IGenericService<ItemType>
    {
        private DatabaseContext _context;

        public ItemTypeService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<ItemType?> Get(int id)
        {
            ItemType? register = await _context.ItemType.FindAsync(id);
            return register; 
        }

        public async Task<List<ItemType>> GetBatch(List<int> ids)
        {
            List<ItemType> result = await _context.ItemType.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<ItemType>> GetAll()
        {
            var query = _context.ItemType.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<ItemType> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(ItemType itemType)
        {
            if(itemType is null) return false;

            var register = await _context.ItemType.FindAsync(itemType.Id);

            if(register is not null) return false;

            await _context.ItemType.AddAsync(itemType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> PostBatch(List<ItemType> itemTypes)
        {
            var results = new List<bool> {};
            foreach (ItemType itemType in itemTypes)
            {
                bool result = await Post(itemType);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(ItemType itemType)
        {
            var DBitemType = await _context.ItemType.FindAsync(itemType.Id);
            if(DBitemType is null) return false;

            DBitemType.Name = itemType.Name;
            DBitemType.Description = itemType.Description;
            DBitemType.Updated_At = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> UpdateBatch(List<ItemType> itemTypes)
        {
            var results = new List<bool> {};
            foreach (ItemType itemType in itemTypes)
            {
                bool result = await Update(itemType);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBitemType = await _context.ItemType.FindAsync(id);
            if(DBitemType is null) return false;

            _context.Remove(DBitemType);
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
