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
            if(warehouse is null) return false;

            var register = await _context.Warehouse.FindAsync(warehouse.Id);

            if(register is not null) return false;

            await _context.Warehouse.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return true;
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
            var DBwarehouse = await _context.Warehouse
                .Include(w => w.Contact) 
                .FirstOrDefaultAsync(w => w.Id == warehouse.Id);

            if(DBwarehouse is null) return false;

            DBwarehouse.Code = warehouse.Code;
            DBwarehouse.Name = warehouse.Name;
            DBwarehouse.Address = warehouse.Address;
            DBwarehouse.Zip = warehouse.Zip;
            DBwarehouse.City = warehouse.City;
            DBwarehouse.Province = warehouse.Province;
            DBwarehouse.Country = warehouse.Country;
            DBwarehouse.Updated_At = DateTime.UtcNow; // Update timestamp to current time

            if (DBwarehouse.Contact is not null && warehouse.Contact is not null)
            {
                //if contact exists change contact
                DBwarehouse.Contact.Name = warehouse.Contact.Name;
                DBwarehouse.Contact.Phone = warehouse.Contact.Phone;
                DBwarehouse.Contact.Email = warehouse.Contact.Email;
            }
            else if (warehouse.Contact is not null)
            {
                // If Contact does not exist but the update includes one, add it
                DBwarehouse.Contact = warehouse.Contact;
            }

            await _context.SaveChangesAsync();
            return true;
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
            if(DBwarehouse is null) return false;

            var contact = DBwarehouse.Contact;
            _context.Remove(DBwarehouse);
            _context.Remove(contact);
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