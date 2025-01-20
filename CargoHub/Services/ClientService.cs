using Microsoft.EntityFrameworkCore;
using CargoHub.Models;

namespace CargoHub.Services{
    public class ClientService : IGenericService<Client>
    {
        private DatabaseContext _context;

        public ClientService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Client?> Get(int id)
        {
            Client? register = await _context.Client.FindAsync(id);
            return register; 
        }

        public async Task<List<Client>> GetBatch(List<int> ids)
        {
            List<Client> result = await _context.Client.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Client>> GetAll()
        {
            var query = _context.Client.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Client> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Client client)
        {
            if(client is null) return false;

            var register = await _context.Client.FindAsync(client.Id);

            if(register is not null) return false;

            await _context.Client.AddAsync(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<bool>> PostBatch(List<Client> clients)
        {
            var results = new List<bool> {};
            foreach (Client client in clients)
            {
                bool result = await Post(client);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Client client)
        {
            var DBclient = await _context.Client.FindAsync(client.Id);

            if(DBclient is null) return false;

            DBclient.Name = client.Name;
            DBclient.Address = client.Address;
            DBclient.City = client.City;
            DBclient.zip_code = client.zip_code;
            DBclient.Province = client.Province;
            DBclient.Country = client.Country;
            DBclient.contact_name = client.contact_name;
            DBclient.contact_email = client.contact_email;
            DBclient.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true; ;
        }

        public async Task<List<bool>> UpdateBatch(List<Client> clients)
        {
            var results = new List<bool> {};
            foreach (Client client in clients)
            {
                bool result = await Update(client);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBclient = await _context.Client.FindAsync(id);
            if(DBclient is null) return false;

            _context.Remove(DBclient);
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