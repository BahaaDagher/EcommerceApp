using Ecommerce.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce
{
    public static class AppConfiguration
    {

        public static void Config(this IServiceCollection services , string connectionString )
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]); 
                //options.UseSqlServer(builder.Configuration["ConnectionStrings : DefaultConnection"]); 
                //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                //options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false; 
                options.SignIn.RequireConfirmedEmail = true;
            })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            //services.AddSingleton<IRepository<Category>, Repository<Category>>();
            //services.AddTransient<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<Brand>, Repository<Brand>>();
            services.AddScoped<IRepository<ProductSubImage>, Repository<ProductSubImage>>();
            services.AddScoped<IRepository<ProductColor>, Repository<ProductColor>>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
