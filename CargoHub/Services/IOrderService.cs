using CargoHub.Models;

namespace CargoHub.Services
{
    public interface IOrderService : IGenericService<Order>
    {
        Task<Order> GetShipmentOrder(int id);
        Task<List<Order>> GetClientOrders(int id);
    }
}