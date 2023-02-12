using Identity.Models;
using Marques.EFCore.SnakeCase;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        //public IdentityDbContext()
        //{

        //}

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ToSnakeCase();
        }
    }
}
