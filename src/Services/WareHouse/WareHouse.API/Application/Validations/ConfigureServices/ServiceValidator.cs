using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models.WareHouse;
using WareHouse.API.Application.Validations.WareHouse;

namespace WareHouse.API.Application.Validations.ConfigureServices
{
    public static class ServiceValidator
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<WareHouseCommands>, WareHouseCommandValidator>();
        }
    }
}