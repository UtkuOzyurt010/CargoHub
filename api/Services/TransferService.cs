using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class TransferService : IGenericService<Transfer>
    {
        private DatabaseContext _context;

        public TransferService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Transfer?> Get(int id)
        {
            Transfer? register = await _context.Transfer.FindAsync(id);
            return register; 
        }

        public async Task<List<Transfer>> GetBatch(List<int> ids)
        {
            List<Transfer> result = await _context.Transfer.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Transfer>> GetAll()
        {
            var query = _context.Transfer.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Transfer> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Transfer transfer)
        {
            if(transfer is not null)
            {
                var register = await _context.Transfer.FindAsync(transfer.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Transfer.AddAsync(transfer);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<bool>> PostBatch(List<Transfer> transfers)
        {
            var results = new List<bool> {};
            foreach (Transfer transfer in transfers)
            {
                bool result = await Post(transfer);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Transfer transfer)
        {
            var DBtransfer = await _context.Transfer.FindAsync(transfer.Id);
            if(DBtransfer is not null)
            {
                DBtransfer = transfer;
                _context.SaveChanges();

                return true;
            }
            else return false;
        }

        public async Task<List<bool>> UpdateBatch(List<Transfer> transfers)
        {
            var results = new List<bool> {};
            foreach (Transfer transfer in transfers)
            {
                bool result = await Update(transfer);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBtransfer = await _context.Transfer.FindAsync(id);
            if(DBtransfer is not null)
            {
                _context.Remove(DBtransfer);
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