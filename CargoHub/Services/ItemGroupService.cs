using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class ItemGroupService : IGenericService<ItemGroup>
    {
        private DatabaseContext _context;

        public ItemGroupService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<ItemGroup?> Get(int id)
        {
            ItemGroup? register = await _context.ItemGroup.FindAsync(id);
            return register; 
        }

        public async Task<List<ItemGroup>> GetBatch(List<int> ids)
        {
            List<ItemGroup> result = await _context.ItemGroup.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<ItemGroup>> GetAll()
        {
            var query = _context.ItemGroup.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<ItemGroup> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(ItemGroup itemGroup)
        {
            if(itemGroup is null) return false;

            var register = await _context.ItemGroup.FindAsync(itemGroup.Id);

            if(register is not null) return false;

            await _context.ItemGroup.AddAsync(itemGroup);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> PostBatch(List<ItemGroup> itemGroups)
        {
            var results = new List<bool> {};
            foreach (ItemGroup itemGroup in itemGroups)
            {
                bool result = await Post(itemGroup);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(ItemGroup itemGroup)
        {
            var DBitemGroup = await _context.ItemGroup.FindAsync(itemGroup.Id);
            
            if(DBitemGroup is null) return false;

            DBitemGroup.Name = itemGroup.Name;
            DBitemGroup.Description = itemGroup.Description;
            DBitemGroup.Updated_At = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> UpdateBatch(List<ItemGroup> itemGroups)
        {
            var results = new List<bool> {};
            foreach (ItemGroup itemGroup in itemGroups)
            {
                bool result = await Update(itemGroup);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBitemGroup = await _context.ItemGroup.FindAsync(id);
            if(DBitemGroup is null) return false;

            _context.Remove(DBitemGroup);
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
