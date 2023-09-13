﻿using MediatR;
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
using Share.Base.Core;
using Infrastructure;
using Share.Base.Core.Filters;
using Microsoft.Extensions.Logging;
using Share.Base.Service.Configuration;
using Share.Base.Service;

namespace WareHouse.API.ConfigureServices.CustomConfiguration
{
    public static class Configuration
    {
        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IElasticSearchClient<>), typeof(ElasticSearchClient<>));        
            services.AddScoped<IFakeData, FakeData>();
            services.AddScoped<IUserSevice, UserSevice>();
            services.AddScoped<ISignalRService, SignalRService>();
            services.AddMediatRCore<Program>();
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));

            services.AddDataBaseContext<WarehouseManagementContext>(configuration, DataConnectionHelper.ConnectionString.Warehouse);
            services.AddDataBaseContext<MasterdataContext>(configuration, DataConnectionHelper.ConnectionString.Master);
            services.AddFilter();
            services.AddSwagger();
        }

    }
}