using CargoHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoHub.Services{
    public class SupplierService : IGenericService<Supplier>
    {
        private DatabaseContext _context;

        public SupplierService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Supplier?> Get(int id)
        {
            Supplier? register = await _context.Supplier.FindAsync(id);
            return register; 
        }

        public async Task<List<Supplier>> GetBatch(List<int> ids)
        {
            List<Supplier> result = await _context.Supplier.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Supplier>> GetAll()
        {
            var query = _context.Supplier.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Supplier> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Supplier supplier)
        {
            if(supplier is null) return false;

            var register = await _context.Supplier.FindAsync(supplier.Id);

            if(register is not null) return false;

            await _context.Supplier.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> PostBatch(List<Supplier> suppliers)
        {
            var results = new List<bool> {};
            foreach (Supplier supplier in suppliers)
            {
                bool result = await Post(supplier);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            var DBsupplier = await _context.Supplier.FindAsync(supplier.Id);
            if(DBsupplier is null) return false;

            DBsupplier.Code = supplier.Code;
            DBsupplier.Name = supplier.Name;
            DBsupplier.Address = supplier.Address;
            DBsupplier.Address_Extra = supplier.Address_Extra;
            DBsupplier.City = supplier.City;
            DBsupplier.Zip_Code = supplier.Zip_Code;
            DBsupplier.Province = supplier.Province;
            DBsupplier.Country = supplier.Country;
            DBsupplier.Contact_Name = supplier.Contact_Name;
            DBsupplier.Phonenumber = supplier.Phonenumber;
            DBsupplier.Reference = supplier.Reference;
            DBsupplier.Updated_At = DateTime.UtcNow;

            // Save changes
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> UpdateBatch(List<Supplier> suppliers)
        {
            var results = new List<bool> {};
            foreach (Supplier supplier in suppliers)
            {
                bool result = await Update(supplier);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBsupplier = await _context.Supplier.FindAsync(id);

            if(DBsupplier is null) return false;

            _context.Remove(DBsupplier);
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