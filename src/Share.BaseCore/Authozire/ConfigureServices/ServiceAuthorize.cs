using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire.ConfigureServices
{
    public static class ServiceAuthorize
    {
        public static void AddBehavior(this IServiceCollection services)
        {
            //   services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped<IGetClaims, GetClaims>();
            services.AddScoped<IAuthozireExtensionForMaster, AuthozireExtensionForMaster>();
        }
    }
}