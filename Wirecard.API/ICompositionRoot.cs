using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wirecard.Business.Commands;
using Wirecard.Business.Services;
using Wirecard.CommandFramework;
using Wirecard.Core.Services;

namespace Wirecard.API
{
    public interface ICompositionRoot
    {
        ILifetimeScope BeginLifetimeScope();
        T Resolve<T>(ILifetimeScope scope);
    }


    public class AutoFacCompositionRoot : ICompositionRoot
    {
        IContainer container;

        public AutoFacCompositionRoot()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<NULLCommandLogger>().As<ICommandLogger>();

            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();


            builder.RegisterType<GetUserCommandHandler>().As<ICommandHandler<GetUserCommand, GetUserResult>>();

            container = builder.Build();

        }

        public ILifetimeScope BeginLifetimeScope()
        {
            return container.BeginLifetimeScope();

        }

        public T Resolve<T>(ILifetimeScope scope)
        {
            return scope.Resolve<T>();
        }
    }


}
