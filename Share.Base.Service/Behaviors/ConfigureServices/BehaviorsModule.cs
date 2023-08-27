

using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Share.Base.Service.Behaviors.ConfigureServices
{
    public class BehaviorsModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            //builder.Register<ServiceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            //});
            var services = new ServiceCollection();

            builder.Populate(services);
            // builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(CachingBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}