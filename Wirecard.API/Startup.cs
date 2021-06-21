using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wirecard.Core.Configuration;
using Wirecard.Core.Models;
using Wirecard.Core.Repositories;
using Wirecard.Core.Services;
using Wirecard.Core.UnitOfWork;
using Wirecard.Data;
using Wirecard.Data.Repositories;
using Wirecard.Business.Services;
using Wirecard.Core.Providers;
using Wirecard.Business.Providers;
using SharedLibrary.Services;
using SharedLibrary.Extensions;
using FluentValidation.AspNetCore;

namespace Wirecard.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Dependency Injection (DI) register
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenProvider, JWTTokenProvider>();

            //Generic Interface Bildirimi
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));

            #endregion

            #region Option Patterns
            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));
            services.Configure<IEnumerable<Client>>(Configuration.GetSection("Clients"));
            #endregion

            #region DbContext
            services.AddDbContext<ApplicationDBContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"), sqlServerOption =>
                {
                    //Migration işlemlerini gerçekleştiren layer
                    sqlServerOption.MigrationsAssembly("Wirecard.Data");
                });
            });
            #endregion

            #region AspNetCore Identity DI Assignment
            services.AddIdentity<UserApp, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();

            //AddDefaultTokenProviders : şifre sıfırlama işlemleri için default token oluşturabilmek için çağırılıyor
            #endregion

            #region Token base authentication Assignment (Gelen tokunun doğruluğunu kontrol eden bilgirimler)
            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
            services.AddCustomTokenAuth(tokenOptions);
            #endregion



            //Fluent Validation Eklendiği için değiştirildi.
            //services.AddControllers();

            services.AddControllers().AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });


            #region Fluent gibi api de kullanılacak hata mesajlarını kendi hazırladığımız response tipinden dönmek için errore ü eziyoruz
            services.UseCustomValidationResponse();
            #endregion


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wirecard.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wirecard.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //Token base işlemler için bu eklenmeli!
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
