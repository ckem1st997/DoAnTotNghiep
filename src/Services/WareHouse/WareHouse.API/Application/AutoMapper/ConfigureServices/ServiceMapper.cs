using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.AutoMapper.WareHouses;

namespace WareHouse.API.Application.AutoMapper.ConfigureServices
{
    public static class ServiceMapper
    {
        public static void AddMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(WareHouseCommandProfile).Assembly);
        }
    }
}