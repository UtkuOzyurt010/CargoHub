using CargoHub.Models;

namespace CargoHub.Services{
    public interface IItemService 
    { 
        Task<Item?> Get(string uid);
        Task<List<Item>> GetBatch(List<string> uids);
        Task<List<Item>> GetAll();
        Task<bool> Post(Item entity);
        Task<List<bool>> PostBatch(List<Item> entities);
        Task<bool> Update(Item entity);
        Task<List<bool>> UpdateBatch(List<Item> entities);
        Task<bool> Delete(string uid);
        Task<List<bool>> DeleteBatch(List<string> uids);
    }
}
