using CargoHub.Models;

namespace CargoHub.Services
{
    public interface ILocationService : IGenericService<Location>
    {
        Task<List<Location>> GetWarehouseLocations(int id);
    }
}
