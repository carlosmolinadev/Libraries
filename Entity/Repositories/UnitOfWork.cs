using Core.Contracts.Persistence;
using Domain.Entities;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWorkEntity
    {
        private readonly TemplateDbContext _context;
        private IRepositoryEntity<Customer> _customer;

        public UnitOfWork(TemplateDbContext context)
        {
            _context = context;
        }

        public IRepositoryEntity<Customer> Customer => _customer ??= new Repository<Customer>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}