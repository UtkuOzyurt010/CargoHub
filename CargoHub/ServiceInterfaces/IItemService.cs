using CargoHub.Models;

namespace CargoHub.Services{
    public interface IItemService 
    { 
        Task<Item?> Get(object uid);
        Task<List<Item>> GetSupplierItems(int id);
        Task<List<Item>> GetOrderItems(List<OrderItem> orderItems);
        Task<List<Item>> GetShipmentItems(List<ShipmentItem> shipmentItems);
        Task<List<Item>> GetTransferItems(List<TransferItem> transferItems);
        Task<List<Item>> GetItemGroupItems(int id);
        Task<List<Item>> GetBatch(List<string> uids);
        Task<List<Item>> GetAll();
        Task<bool> Post(Item entity);
        Task<List<bool>> PostBatch(List<Item> entities);
        Task<bool> Update(Item entity);
        Task<List<bool>> UpdateBatch(List<Item> entities);
        Task<bool> Delete(object uid);
        Task<List<bool>> DeleteBatch(List<string> uids);
    }
}
