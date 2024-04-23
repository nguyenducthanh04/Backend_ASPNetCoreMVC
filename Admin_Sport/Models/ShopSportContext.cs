using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Admin_Sport.Models
{
    public class ShopSportContext : DbContext
    {
        public ShopSportContext(DbContextOptions<ShopSportContext> options) : base(options)
        {

        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Images> Images { get; set; }
    }
}
