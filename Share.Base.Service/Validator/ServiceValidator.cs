using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Share.Base.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Share.Base.Service.Validator
{
    public static class ServiceValidator
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<BaseSearchModel>, BaseSearchModelValidator>();
        }
    }
}