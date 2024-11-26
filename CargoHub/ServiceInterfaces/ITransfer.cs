using CargoHub.Models;

namespace CargoHub.Services{
    public interface ITransfer 
    { 
        Task<ReadTransferDto?> Get(int id);
        Task<List<Transfer>> GetBatch(List<int> ids);
        Task<List<Transfer>> GetAll();
        Task<bool> Post(Transfer entity);
        Task<List<bool>> PostBatch(List<Transfer> entities);
        Task<bool> Update(Transfer entity);
        Task<List<bool>> UpdateBatch(List<Transfer> entities);
        Task<bool> Delete(int id);
        Task<List<bool>> DeleteBatch(List<int> ids);
    }
}
