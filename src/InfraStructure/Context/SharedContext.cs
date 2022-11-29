using InfraStructure.Model.Checkout;
using InfraStructure.Model.ClientAdm;
using InfraStructure.Model.Invoice;
using InfraStructure.Model.Payment;
using InfraStructure.Model.ProductAdm;
using Microsoft.EntityFrameworkCore;
using Product = InfraStructure.Model.Invoice.Product;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceProduct>().HasKey(sc => new { sc.InvoiceId, sc.ProductId });

            modelBuilder.Entity<InvoiceProduct>()
                .HasOne<Invoice>(sc => sc.Invoice)
                .WithMany(s => s.Items)
                .HasForeignKey(sc => sc.ProductId);


            modelBuilder.Entity<InvoiceProduct>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.InvoiceProduct)
                .HasForeignKey(sc => sc.InvoiceId);
        }
        public DbSet<ProductModel> ProductsAdm { get; set; }
        public DbSet<Model.StoreCatalog.ProductModel> ProductsCatalog { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InvoiceProduct> InvoiceProducts { get; set; }

        public DbSet<Model.Checkout.Product> CheckoutProducts { get; set; }
        public DbSet<Model.Checkout.Client> CheckoutClients { get; set; }
    }
}
