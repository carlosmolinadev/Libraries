using Core.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Persistance.Repositories;

namespace Template.Infrastructure.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TemplateDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWorkEntity, UnitOfWork>();
            //services.AddScoped<ICustomerRepositoryEntity, CustomerRepository>();
            //services.AddScoped(typeof(IRepositoryEntity<>), typeof(Repository<>));



            //services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
