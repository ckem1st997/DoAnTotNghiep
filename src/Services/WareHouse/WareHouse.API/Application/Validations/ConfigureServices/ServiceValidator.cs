﻿using FluentValidation;
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

namespace WareHouse.API.Application.Validations.ConfigureServices
{
    public static class ServiceValidator
    {
        public static void AddValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<WareHouseCommands>, WareHouseCommandValidator>();   
        }
    }
}