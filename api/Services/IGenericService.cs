namespace api.Services
{
    public interface IGenericService<T> 
    { 
        Task<T?> Get(int id);
        Task<List<T>> GetBatch(List<int> ids);
        Task<List<T>> GetAll();
        Task<bool> Post(T entity);
        Task<List<bool>> PostBatch(List<T> entities);
        Task<bool> Update(T entity);
        Task<List<bool>> UpdateBatch(List<T> entities);
        Task<bool> Delete(int id);
        Task<List<bool>> DeleteBatch(List<int> ids);
    }
}
