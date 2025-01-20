using CargoHub.Models;

namespace CargoHub.Services
{
    public interface IOrderService
    {
        Task<ReadOrderDto?> Get(int id);
        Task<List<Order>> GetBatch(List<int> ids);
        Task<List<Order>> GetAll();
        Task<bool> Post(Order entity);
        Task<List<bool>> PostBatch(List<Order> entities);
        Task<bool> Update(Order entity);
        Task<List<bool>> UpdateBatch(List<Order> entities);
        Task<bool> Delete(int id);
        Task<List<bool>> DeleteBatch(List<int> ids);
        Task<Order> GetShipmentOrder(int id);
        Task<List<Order>> GetClientOrders(int id);
    }
}