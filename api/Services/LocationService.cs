using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class LocationService : IGenericService<Location>
    {
        private DatabaseContext _context;

        public LocationService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Location?> Get(int id)
        {
            Location? register = await _context.Location.FindAsync(id);
            return register; 
        }

        public async Task<List<Location>> GetBatch(List<int> ids)
        {
            List<Location> result = await _context.Location.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Location>> GetAll()
        {
            var query = _context.Location.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Location> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Location location)
        {
            if(location is not null)
            {
                var register = await _context.Location.FindAsync(location.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Location.AddAsync(location);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<bool>> PostBatch(List<Location> locations)
        {
            var results = new List<bool> {};
            foreach (Location location in locations)
            {
                bool result = await Post(location);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Location location)
        {
            var DBlocation = await _context.Location.FindAsync(location.Id);
            if(DBlocation is not null)
            {
                DBlocation = location;
                _context.SaveChanges();

                return true;
            }
            else return false;
        }

        public async Task<List<bool>> UpdateBatch(List<Location> locations)
        {
            var results = new List<bool> {};
            foreach (Location location in locations)
            {
                bool result = await Update(location);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBlocation = await _context.Location.FindAsync(id);
            if(DBlocation is not null)
            {
                _context.Remove(DBlocation);
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
