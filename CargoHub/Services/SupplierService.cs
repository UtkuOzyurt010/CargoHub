using Models;
using Microsoft.EntityFrameworkCore;

namespace Services{
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
            if(supplier is not null)
            {
                var register = await _context.Supplier.FindAsync(supplier.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Supplier.AddAsync(supplier);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
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
            if(DBsupplier is not null)
            {
                DBsupplier = supplier;
                _context.SaveChanges();

                return true;
            }
            else return false;
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
            if(DBsupplier is not null)
            {
                _context.Remove(DBsupplier);
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