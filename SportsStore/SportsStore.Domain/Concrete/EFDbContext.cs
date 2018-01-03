using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        static EFDbContext()
        {
            // https://farooqmdotcom.wordpress.com/2013/10/24/entity-framework-6-weirdness/
            // Without this line, EF6 will break.
            // also need to add reference to EntityFramework.SqlServer under ef package lib dir
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public DbSet<Product> Products { get; set; }
    }
}