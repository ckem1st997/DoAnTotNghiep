using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.API.Application.Validations;
using WareHouse.API.Application.Validations.BaseValidator;

namespace WareHouse.API.Application.Validations.ConfigureServices
{
    public static class ServiceValidator
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.AddFluentValidation();
            services.AddTransient<IValidator<WareHouseCommands>, WareHouseCommandValidator>();
            services.AddTransient<IValidator<WareHouseItemCommands>, WareHouseItemCommandValidator>();
            services.AddTransient<IValidator<WareHouseItemCategoryCommands>, WareHouseItemCategoryCommandValidator>();
            services.AddTransient<IValidator<UnitCommands>, UnitCommandValidator>();
            services.AddTransient<IValidator<VendorCommands>, VendorCommandValidator>();
            services.AddTransient<IValidator<BaseSearchModel>, BaseSearchModelValidator>();
        }
    }
}