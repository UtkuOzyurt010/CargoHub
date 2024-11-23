using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class WarehouseService : IGenericService<Warehouse>
    {
        private DatabaseContext _context;

        public WarehouseService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Warehouse?> Get(int id)
        {
            Warehouse? register = await _context.Warehouse.FindAsync(id);
            return register; 
        }

        public async Task<List<Warehouse>> GetBatch(List<int> ids)
        {
            List<Warehouse> result = await _context.Warehouse.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Warehouse>> GetAll()
        {
            var query = _context.Warehouse.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Warehouse> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Warehouse warehouse)
        {
            if(warehouse is not null)
            {
                var register = await _context.Warehouse.FindAsync(warehouse.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Warehouse.AddAsync(warehouse);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<bool>> PostBatch(List<Warehouse> warehouses)
        {
            var results = new List<bool> {};
            foreach (Warehouse warehouse in warehouses)
            {
                bool result = await Post(warehouse);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Warehouse warehouse)
        {
            var DBwarehouse = await _context.Warehouse.FindAsync(warehouse.Id);
            if(DBwarehouse is not null)
            {
                DBwarehouse = warehouse;
                _context.SaveChanges();

                return true;
            }
            else return false;
        }

        public async Task<List<bool>> UpdateBatch(List<Warehouse> warehouses)
        {
            var results = new List<bool> {};
            foreach (Warehouse warehouse in warehouses)
            {
                bool result = await Update(warehouse);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBwarehouse = await _context.Warehouse
                                .Include(w => w.Contact)
                                .FirstOrDefaultAsync(w => w.Id == id);
            var contact = DBwarehouse.Contact;

            if(DBwarehouse is not null)
            {
                _context.Remove(DBwarehouse);
                _context.Remove(contact);
                await _context.SaveChangesAsync();
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