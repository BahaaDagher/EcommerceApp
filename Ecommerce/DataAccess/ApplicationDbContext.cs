using Ecommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ecommerce.ViewModel;

namespace Ecommerce.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductSubImage> ProductSubImages { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        //override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;initial catalog = Ecommerce520 ;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductSubImage>().HasKey(p=>new { p.ProductId , p.Img}); 
            modelBuilder.Entity<ProductColor>().HasKey(p=>new { p.ProductId , p.Color}); 
            modelBuilder.Entity<Brand>().Property(b=>b.Img).HasDefaultValue("defaultImg.png"); 
        }
        public DbSet<Ecommerce.ViewModel.RegisterVM> RegisterVM { get; set; } = default!;
        public DbSet<Ecommerce.ViewModel.LoginVm> LoginVm { get; set; } = default!;
        public DbSet<Ecommerce.ViewModel.ResendEmailConfirmationVM> ResendEmailConfirmationVM { get; set; } = default!;
    }
}
