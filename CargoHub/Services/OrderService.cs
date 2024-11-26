using CargoHub.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CargoHub.Services{
    public class OrderService : IOrderService
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

        public async Task<Order> GetShipmentOrder(int id)
        {
            var result = await _context.Order.FindAsync(id);
            return result;
        }

        public async Task<List<Order>> GetClientOrders(int id)
        {
            var result = await _context.Order.Where(order => order.Ship_To == id || order.Bill_To == id).ToListAsync();
            return result;
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
            if(order is null) return false;

            var register = await _context.Order.FindAsync(order.Id);

            if(register is not null) return false;

            if (string.IsNullOrEmpty(order.ItemsJson))
            order.ItemsJson = JsonConvert.SerializeObject(order.Items);

            await _context.Order.AddAsync(order);
            _context.SaveChanges();
            return true;
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

            if(DBorder is null) return false;

            if (string.IsNullOrEmpty(order.ItemsJson))
            order.ItemsJson = JsonConvert.SerializeObject(order.Items);

            DBorder.Source_Id = order.Source_Id;
            DBorder.Order_Date = order.Order_Date;
            DBorder.Request_Date = order.Request_Date;
            DBorder.Reference = order.Reference;
            DBorder.Reference_Extra = order.Reference_Extra;
            DBorder.Order_Status = order.Order_Status;
            DBorder.Notes = order.Notes;
            DBorder.Shipping_Notes = order.Shipping_Notes;
            DBorder.Picking_Notes = order.Picking_Notes;
            DBorder.Warehouse_Id = order.Warehouse_Id;
            DBorder.Ship_To = order.Ship_To;
            DBorder.Bill_To = order.Bill_To;
            DBorder.Shipment_Id = order.Shipment_Id;
            DBorder.Total_Amount = order.Total_Amount;
            DBorder.Total_Discount = order.Total_Discount;
            DBorder.Total_Tax = order.Total_Tax;
            DBorder.Total_Surcharge = order.Total_Surcharge;
            DBorder.ItemsJson = order.ItemsJson;
            DBorder.Updated_At = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
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
            if(DBorder is null) return false;

            _context.Remove(DBorder);
            _context.SaveChanges();
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