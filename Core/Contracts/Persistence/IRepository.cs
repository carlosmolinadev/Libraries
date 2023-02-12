namespace Core.Contracts.Persistence
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IList<T>> GetFilteredAsync(QueryFilter filter);
    }
}