using CargoHub.Models;

namespace CargoHub.Services
{
    public interface IInventoryService : IGenericService<Inventory>
    {
        Task<Inventory> GetItemInventory(string uid);
        Task<dynamic> GetItemInventoryTotals(string uid);
    }
}