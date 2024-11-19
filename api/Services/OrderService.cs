using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class OrderService : IGenericService<Order>
    {
        private DatabaseContext _context;

        public OrderService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Order?> Get(int id)
        {
            Order? register = await _context.Order.FindAsync(id);
            return register; 
        }

        public async Task<List<Order>> GetBatch(List<int> ids)
        {
            List<Order> result = await _context.Order.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Order>> GetAll()
        {
            var query = _context.Order.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Order> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Order order)
        {
            if(order is not null)
            {
                var register = await _context.Order.FindAsync(order.Id);

                if(register is not null)
                {
                    return false;
                }
                else
                {
                    await _context.Order.AddAsync(order);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<List<bool>> PostBatch(List<Order> orders)
        {
            var results = new List<bool> {};
            foreach (Order order in orders)
            {
                bool result = await Post(order);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Order order)
        {
            var DBorder = await _context.Order.FindAsync(order.Id);
            if(DBorder is not null)
            {
                DBorder = order;
                _context.SaveChanges();

                return true;
            }
            else return false;
        }

        public async Task<List<bool>> UpdateBatch(List<Order> orders)
        {
            var results = new List<bool> {};
            foreach (Order order in orders)
            {
                bool result = await Update(order);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBorder = await _context.Order.FindAsync(id);
            if(DBorder is not null)
            {
                _context.Remove(DBorder);
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