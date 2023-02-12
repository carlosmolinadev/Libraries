
using Domain.Entities;

namespace Core.Contracts.Persistence
{
    public interface ICustomerRepositoryEntity : IRepositoryEntity<Customer>
    {
        public Task CustomImplementation(Customer customer);
    }
}
