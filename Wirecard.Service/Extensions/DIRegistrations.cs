
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wirecard.Business.Commands;
using Wirecard.Business.Providers;
using Wirecard.Business.Services;
using Wirecard.CommandFramework;
using Wirecard.Core.Providers;
using Wirecard.Core.Repositories;
using Wirecard.Core.Services;
using Wirecard.Core.UnitOfWork;
using Wirecard.Data;
using Wirecard.Data.Repositories;

namespace Wirecard.Business.Extensions
{
    public static class DIRegistrations
    {
        public static void AddCustomScopes(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenProvider, JWTTokenProvider>();
            services.AddScoped<ICommandLogger, NULLCommandLogger>();


            //Generic Interface Bildirimi
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
        }
    }
}
