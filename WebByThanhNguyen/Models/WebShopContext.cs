using Microsoft.EntityFrameworkCore;

namespace WebByThanhNguyen.Models
{
    public class WebShopContext:DbContext
    {
        public WebShopContext(DbContextOptions<WebShopContext> options) : base(options) { }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Images> Images { get; set; }

    }
}
