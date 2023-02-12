using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Persistence
{
    public interface IRepositoryEntity<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IReadOnlyList<T>> GetFilteredAsync(QueryFilter filter);
    }
}
