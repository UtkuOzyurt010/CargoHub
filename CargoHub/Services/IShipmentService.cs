using CargoHub.Models;

namespace CargoHub.Services{
    public interface IShipmentService
    { 
        Task<ReadShipmentDto?> Get(int id);
        Task<List<Shipment>> GetBatch(List<int> ids);
        Task<List<Shipment>> GetAll();
        Task<bool> Post(Shipment entity);
        Task<List<bool>> PostBatch(List<Shipment> entities);
        Task<bool> Update(Shipment entity);
        Task<List<bool>> UpdateBatch(List<Shipment> entities);
        Task<bool> Delete(int id);
        Task<List<bool>> DeleteBatch(List<int> ids);
    }
}
