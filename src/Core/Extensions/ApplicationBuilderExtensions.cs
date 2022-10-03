using Microsoft.AspNetCore.Builder;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace Core.Extensions
{
    /// <summary>
    /// Represents extensions of IApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        //public static void ConfigureDBContext(this IServiceCollection application, WebApplicationBuilder web)
        //{
        //    web.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

        //    web.Host.ConfigureContainer<ContainerBuilder>(builder =>
        //    {
        //        // Declare your services with proper lifetime

        //        builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
        //        builder.RegisterType<BaseEngine>().As<IEngine>().SingleInstance();

        //        // truyền vào tên service và giá trịc cần truyền vào trong hàm khởi tạo
        //        builder.RegisterGeneric(typeof(TTT<>)).Named("Master", typeof(ITTT<>)).WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)((pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "key"), (Func<ParameterInfo, IComponentContext, object>)((pi, ctx) => "master"))).InstancePerLifetimeScope();
        //        builder.RegisterGeneric(typeof(TTT<>)).Named("wh", typeof(ITTT<>)).WithParameter<object, ReflectionActivatorData, DynamicRegistrationStyle>(new ResolvedParameter((Func<ParameterInfo, IComponentContext, bool>)((pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "key"), (Func<ParameterInfo, IComponentContext, object>)((pi, ctx) => "wh"))).InstancePerLifetimeScope();
        //        builder.RegisterGeneric(typeof(TTT<>)).As(typeof(ITTT<>)).InstancePerLifetimeScope();
        //    });
        //}
    }
}
