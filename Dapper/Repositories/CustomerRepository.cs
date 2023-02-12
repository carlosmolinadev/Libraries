using Core.Contracts.Persistence;
using Domain.Entities;
using Npgsql;
using System.Data;
using System.Data.Common;

namespace Template.Infrastructure.Persistance.Dapper.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbConnection connection, IUnitOfWork unitOfWork) : base(connection, unitOfWork)
        {
        }

        public async Task CustomImplementation(Customer customer)
        {
            await base.AddAsync(customer);
        }
    }
}
