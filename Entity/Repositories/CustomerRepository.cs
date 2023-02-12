using Core.Contracts.Persistence;
using Domain.Entities;

namespace Persistance.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepositoryEntity
    {
        public CustomerRepository(TemplateDbContext dbContext) : base(dbContext)
        {
        }

        public Task CustomImplementation(Customer customer)
        {
            _dbContext.Set<Customer>().AddAsync(customer);
            _dbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
