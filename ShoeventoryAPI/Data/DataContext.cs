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


    }
}
