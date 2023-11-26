using Autofac;
using Share.Base.Core.AutoDependencyInjection.InjectionAttribute;
using Share.Base.Core.Extensions;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Share.Base.Core.AutoDependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static void RegisterDIContainer(this ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            foreach (var assembly in assemblies)
            {
                #region auto register with Transient
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.GetCustomAttribute<TransientDependencyAttribute>() != null)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
                #endregion

                #region auto register with Scoped
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.GetCustomAttribute<ScopedDependencyAttribute>() != null)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
                #endregion

                #region auto register with Singleton
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.GetCustomAttribute<SingletonDependencyAttribute>() != null)
                    .AsImplementedInterfaces()
                    .SingleInstance();
                #endregion
            }
        }
    }
}
