using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Persistence
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task CustomImplementation(Customer customer);
    }
}
