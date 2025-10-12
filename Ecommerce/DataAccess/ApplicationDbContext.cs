using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductSubImage> ProductSubImages { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;initial catalog = Ecommerce520 ;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductSubImage>().HasKey(p=>new { p.ProductId , p.Img}); 
            modelBuilder.Entity<ProductColor>().HasKey(p=>new { p.ProductId , p.Color}); 
            modelBuilder.Entity<Brand>().Property(b=>b.Img).HasDefaultValue("defaultImg.png"); 
        }
    }
}
