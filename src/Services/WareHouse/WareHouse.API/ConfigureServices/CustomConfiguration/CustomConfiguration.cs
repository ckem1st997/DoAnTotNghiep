using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Controllers;
using WareHouse.Infrastructure;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Authentication;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authorization;
using WareHouse.API.Application.SignalRService;
using WareHouse.API.Infrastructure.ElasticSearch;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using EasyCaching.Core;
using Aspose.Diagram;
using Share.BaseCore.Repositories;
using Share.BaseCore;
using Infrastructure;
using Share.BaseCore.Filters;
using Share.BaseCore.Behaviors;
using Microsoft.Extensions.Logging;
using Share.BaseCore.CustomConfiguration;

namespace WareHouse.API.ConfigureServices.CustomConfiguration
{
    public static class CustomConfiguration
    {
        public static void AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IElasticSearchClient<>), typeof(ElasticSearchClient<>));        
            services.AddScoped<IFakeData, FakeData>();
            services.AddScoped<IUserSevice, UserSevice>();
            services.AddScoped<ISignalRService, SignalRService>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));

            services.AddCustomConfigurationCore<WareHousesController, WarehouseManagementContext, Startup>(configuration, "WarehouseManagementContext");
            services.AddGenericRepository<WarehouseManagementContext>();
            services.AddQueryRepository<WarehouseManagementContext>();

        }

        public static void ConfigureDBContext(this ContainerBuilder builder)
        {

        }
    }
}