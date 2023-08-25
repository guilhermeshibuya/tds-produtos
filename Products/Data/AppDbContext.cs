using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("DataSource=products.db;Cache=Shared");
    }
}
