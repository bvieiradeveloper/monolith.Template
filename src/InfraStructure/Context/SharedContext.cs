using InfraStructure.Model.ClientAdm;
using InfraStructure.Model.ProductAdm;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Context
{
    public class SharedContext: DbContext
    {
        public SharedContext()
        {

        }

        public SharedContext(DbContextOptions<SharedContext> options) : base(options) 
        {
        }

        public DbSet<ProductModel> ProductsAdm { get; set; }
        public DbSet<Model.StoreCatalog.ProductModel> ProductsCatalog { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
    }
}
