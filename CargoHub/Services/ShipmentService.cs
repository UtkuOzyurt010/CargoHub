using CargoHub.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CargoHub.Services{
    public class ShipmentService : IGenericService<Shipment>
    {
        private DatabaseContext _context;

        public ShipmentService(DatabaseContext context)
        {
            _context = context;
        } 

        public async Task<Shipment?> Get(int id)
        {
            Shipment? register = await _context.Shipment.FindAsync(id);
            return register; 
        }

        public async Task<List<Shipment>> GetBatch(List<int> ids)
        {
            List<Shipment> result = await _context.Shipment.
                                        Where(x=>ids.Contains(x.Id)).
                                        ToListAsync();
            return result;
        }

        public async Task<List<Shipment>> GetAll()
        {
            var query = _context.Shipment.AsQueryable();

            // add filter queries here: format: if (filter is not null) query = Where(x => x == check the checks with your filter)

            List<Shipment> result = await query.ToListAsync();

            return result;
        }

        public async Task<bool> Post(Shipment shipment)
        {
            if(shipment is null) return false;

            var register = await _context.Shipment.FindAsync(shipment.Id);

            if(register is not null) return false;

            await _context.Shipment.AddAsync(shipment);
            _context.SaveChanges();
            return true;
            
        }

        public async Task<List<bool>> PostBatch(List<Shipment> shipments)
        {
            var results = new List<bool> {};
            foreach (Shipment shipment in shipments)
            {
                bool result = await Post(shipment);
                results.Add(result);
            }
            return results;
        }

        public async Task<bool> Update(Shipment shipment)
        {
            var DBshipment = await _context.Shipment.FindAsync(shipment.Id);
            if (DBshipment != null)
            {
                DBshipment.Order_Id = shipment.Order_Id;
                DBshipment.Source_Id = shipment.Source_Id;
                DBshipment.Order_Date = shipment.Order_Date;
                DBshipment.Request_Date = shipment.Request_Date;
                DBshipment.Shipment_Date = shipment.Shipment_Date;
                DBshipment.Shipment_Type = shipment.Shipment_Type;
                DBshipment.Shipment_Status = shipment.Shipment_Status;
                DBshipment.Notes = shipment.Notes;
                DBshipment.Carrier_Code = shipment.Carrier_Code;
                DBshipment.Carrier_Description = shipment.Carrier_Description;
                DBshipment.Service_Code = shipment.Service_Code;
                DBshipment.Payment_Type = shipment.Payment_Type;
                DBshipment.Transfer_Mode = shipment.Transfer_Mode;
                DBshipment.Total_Package_Count = shipment.Total_Package_Count;
                DBshipment.Total_Package_Weight = shipment.Total_Package_Weight;
                DBshipment.Created_At = shipment.Created_At;
                DBshipment.Updated_At = DateTime.UtcNow; // Update the timestamp

                // Ensure ItemsJson is correctly updated (you can handle this if necessary)
                DBshipment.ItemsJson = shipment.ItemsJson;

                // Save changes to the database
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<bool>> UpdateBatch(List<Shipment> shipments)
        {
            var results = new List<bool> {};
            foreach (Shipment shipment in shipments)
            {
                bool result = await Update(shipment);
                results.Add(result);
            }

            return results;
        }

        public async Task<bool> Delete(int id)
        {
            var DBshipment = await _context.Shipment.FindAsync(id);
            if(DBshipment is not null)
            {
                _context.Remove(DBshipment);
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