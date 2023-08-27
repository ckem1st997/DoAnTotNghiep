using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Share.Base.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareImplemention
{

    public static class ServiceConfigImplementAPI
    {
        public static void AddServiceConfigImplementAPI(this IServiceCollection services)
        {
            services.AddScoped<IUserInfomationService, UserInfomationService>();
        }
    }
}
