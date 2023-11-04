using Autofac;
using Autofac.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using WareHouse.Infrastructure;
using Nest;
using Infrastructure;
using Share.Base.Core.Infrastructure;
using Share.Base.Service.Configuration;
using Share.Base.Service;

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

            builder.AddDbContext<WarehouseManagementContext>(DataConnectionHelper.ConnectionStringNames.Warehouse);
          //  builder.AddDbContext<MasterdataContext>(DataConnectionHelper.ConnectionStringNames.Master);

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

        }
    }



}
