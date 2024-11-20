using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class ItemLineService : IGenericService<ItemLine>
    {
        private DatabaseContext _context;

        public ItemLineService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<ItemLine?> Get(int id)
        {
            ItemLine? register = await _context.ItemLine.FindAsync(id);
            return register; 
        }

        public async Task<List<ItemLine>> GetBatch(List<int> ids)
        {
            List<ItemLine> result = await _context.ItemLine.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<ItemLine>> GetAll()
        {
            var query = _context.ItemLine.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<ItemLine> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(ItemLine itemLine)
        {
            if(itemLine is not null)
            {
                var register = await _context.ItemLine.FindAsync(itemLine.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.ItemLine.AddAsync(itemLine);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<bool>> PostBatch(List<ItemLine> itemLines)
        {
            var results = new List<bool> {};
            foreach (ItemLine itemLine in itemLines)
            {
                bool result = await Post(itemLine);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(ItemLine itemLine)
        {
            var DBitemLine = await _context.ItemLine.FindAsync(itemLine.Id);
            if(DBitemLine is not null)
            {
                DBitemLine = itemLine;
                _context.SaveChanges();

                return true;
            }
            else return false;
        }

        public async Task<List<bool>> UpdateBatch(List<ItemLine> itemLines)
        {
            var results = new List<bool> {};
            foreach (ItemLine itemLine in itemLines)
            {
                bool result = await Update(itemLine);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBitemLine = await _context.ItemLine.FindAsync(id);
            if(DBitemLine is not null)
            {
                _context.Remove(DBitemLine);
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
