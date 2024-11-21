using CargoHub.Models;

namespace CargoHub.Services
{
    public interface IInventoryService : IGenericService<Inventory>
    {
        Task<List<Inventory>> GetItemInventory(string uid);
    }
}