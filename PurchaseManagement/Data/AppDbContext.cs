using Microsoft.EntityFrameworkCore;
using PurchaseManagement.Models;

namespace PurchaseManagement.Data
{
    public class AppDbContext : DbContext
    {

        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            
        }
        
        public DbSet<Client> Tb_Client { get; set; }
        public DbSet<Product> Tb_Products { get; set; }
        public DbSet<Purchase> Tb_Purchases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>()
            .HasOne(c => c.Client)
            .WithMany(p => p.Purchases)
            .HasForeignKey(c => c.ClientId)
            .IsRequired();

            modelBuilder.Entity<Purchase>()
            .HasOne(pr => pr.Product)
            .WithMany(p => p.Purchases)
            .HasForeignKey(pr => pr.ProductId)
            .IsRequired();
            
        }
    }
}
