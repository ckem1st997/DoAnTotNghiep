using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Share.BaseCore.Extensions
{
    public static class GetServiceByInterface<T>
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static T GetService()
        {
            _httpContextAccessor = new HttpContextAccessor();
            var type = _httpContextAccessor.HttpContext.RequestServices.GetService<T>();
            return type;
        }
    }
}
