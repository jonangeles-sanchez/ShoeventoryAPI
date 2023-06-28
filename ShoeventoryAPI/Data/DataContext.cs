using Microsoft.EntityFrameworkCore;
using ShoeventoryAPI.Models;

namespace ShoeventoryAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<ShoeCollection> ShoeCollections { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<SoldShoe> SoldShoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchant>()
                .HasMany(m => m.Transactions)
                .WithOne(t => t.Merchant)
                .HasForeignKey(t => t.MerchantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShoeCollection>()
                .HasMany(sc => sc.Transactions)
                .WithOne(t => t.ShoeCollection)
                .HasForeignKey(t => t.ShoeCollectionId)
                .OnDelete(DeleteBehavior.NoAction);

            // Add other entity configurations...

            base.OnModelCreating(modelBuilder);
        }

    }
}
