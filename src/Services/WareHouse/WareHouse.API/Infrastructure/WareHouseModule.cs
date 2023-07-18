using Autofac;
using Autofac.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Share.BaseCore.Behaviors;
using Share.BaseCore.Repositories;
using System;
using WareHouse.Infrastructure;
using Nest;
using Share.BaseCore.IRepositories;
using Share.BaseCore.CustomConfiguration;
using Infrastructure;
using Share.BaseCore.BaseNop;

namespace WareHouse.API.Infrastructure
{
    public class WareHouseModule : Module
    {
        public WareHouseModule()
        {
        }
        protected override void Load(ContainerBuilder builder)
        {
            // Declare your services with proper lifetime
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().InstancePerLifetimeScope();
            builder.RegisterType<BaseEngine>().As<IEngine>().SingleInstance();
            //
            //builder.RegisterType<WarehouseManagementContext>()
            //     .Named<DbContext>(DataConnectionHelper.ConnectionStringNames.Warehouse)
            //     .InstancePerDependency();

            builder.AddDbContext<WarehouseManagementContext>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            builder.AddDbContext<MasterdataContext>(DataConnectionHelper.ConnectionStringNames.Master);

            // mulplite connect to dbcontext
            //builder.RegisterType<MasterdataContext>()
            //    .Named<DbContext>(DataConnectionHelper.ConnectionStringNames.Master)
            //    .InstancePerDependency();


            // Register resolving delegate 
            //builder.Register<Func<string, DbContext>>(c =>
            //{
            //    var cc = c.Resolve<IComponentContext>();
            //    return connectionStringName => cc.ResolveNamed<DbContext>(connectionStringName);
            //});
            //builder.Register<Func<string, Lazy<DbContext>>>(c =>
            //{
            //    var cc = c.Resolve<IComponentContext>();
            //    return connectionStringName => cc.ResolveNamed<Lazy<DbContext>>(connectionStringName);
            //});

            builder.AddRegisterDbContext(true);
            //
            // ef core

            builder.AddGeneric(DataConnectionHelper.ConnectionStringNames.Warehouse, DataConnectionHelper.ParameterName);
            builder.AddGeneric(DataConnectionHelper.ConnectionStringNames.Master, DataConnectionHelper.ParameterName);

            //builder.RegisterGeneric(typeof(RepositoryEF<>))
            //      .Named(DataConnectionHelper.ConnectionStringNames.Warehouse, typeof(IRepositoryEF<>))
            //      .WithParameter(new ResolvedParameter(
            //          // kiểu truyền vào và tên biến truyền vào qua hàm khởi tạo
            //          (pi, ctx) => pi.ParameterType == typeof(DbContext) && pi.Name == DataConnectionHelper.ParameterName,
            //          (pi, ctx) => EngineContext.Current.Resolve<DbContext>(DataConnectionHelper.ConnectionStringNames.Warehouse)))
            //.InstancePerLifetimeScope();


            // mulplite connect to dbcontext
            //builder.RegisterGeneric(typeof(RepositoryEF<>))
            //      .Named(DataConnectionHelper.ConnectionStringNames.Master, typeof(IRepositoryEF<>))
            //      .WithParameter(new ResolvedParameter(
            //          (pi, ctx) => pi.ParameterType == typeof(DbContext) && pi.Name == DataConnectionHelper.ParameterName,
            //          (pi, ctx) => EngineContext.Current.Resolve<DbContext>(DataConnectionHelper.ConnectionStringNames.Master)))
            //      .InstancePerLifetimeScope();



            // Behavior
            //   builder.RegisterGeneric(typeof(LoggingBehavior<,>))
            //.Named(DataConnectionHelper.ConnectionStringNames.Warehouse, typeof(IPipelineBehavior<,>))
            //.WithParameter(new ResolvedParameter(
            //    // kiểu truyền vào và tên biến truyền vào qua hàm khởi tạo
            //    (pi, ctx) => pi.ParameterType == typeof(DbContext) && pi.Name == DataConnectionHelper.ParameterName,
            //    (pi, ctx) => EngineContext.Current.Resolve<DbContext>(DataConnectionHelper.ConnectionStringNames.Warehouse)))
            //.InstancePerDependency();
            //   builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }



}
