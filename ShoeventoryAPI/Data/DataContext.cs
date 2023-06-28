using Microsoft.EntityFrameworkCore;

namespace ShoeventoryAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
