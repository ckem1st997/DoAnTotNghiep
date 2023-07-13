using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Master.Filters;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Master.Extension;
using Master.Service;
using Master.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Master.SignalRHubs;
using MediatR;
using Share.BaseCore.CustomConfiguration;
using Master.Controllers;

namespace Master.ConfigureServices.CustomConfiguration
{
    public static class CustomConfiguration
    {
        public static void AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped(typeof(IPaginatedList<>), typeof(PaginatedList<>));
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddCustomConfigurationCore<MasterdataContext, Startup>(configuration, "MasterdataContext");
            services.AddConfigurationCoreFilter<Startup>();
            services.AddSwaggerCore();
        }

    }
}
