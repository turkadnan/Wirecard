using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Core.Models;

namespace Wirecard.Data
{
    public class ApplicationDBContext : IdentityDbContext<UserApp, IdentityRole, string>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //ref https://gist.github.com/scottsauber/77a1427bde3b38cf96fed87aaabd2e92

            //aynı assembl deki tüm build edeceği interfaceleri arayıp configuration ları implement edecek.
            //örn \Configurations\ProductConfiguration.cs ve aynı klasördeki tüm conf ları alıp tabloları conf lardaki kurallara göre oluşturacak.
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Yada tek tek aşağıdaki gibi
            // builder.ApplyConfiguration(new Configurations.ProductConfiguration());
            // builder.ApplyConfiguration(new Configurations.UserAppConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
