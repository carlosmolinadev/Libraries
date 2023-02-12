using Domain.Entities;

namespace Core.Contracts.Persistence
{
    public interface IUnitOfWorkEntity : IDisposable
    {
        Task Save();
        IRepositoryEntity<Customer> Customer { get; }
    }
}
