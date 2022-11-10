﻿using Autofac;
using Base.Events;
using Master.IntegrationEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Share.BaseCore.EventBus;
using Share.BaseCore.EventBus.Abstractions;
using Share.BaseCore.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.IntegrationEvents
{
    public static class EventBusKafKa
    {
        public static void AddEventBusKafka(this IServiceCollection services)
        {
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            // thêm xử lí event trong app
            services.AddTransient<CreateHistoryIntegrationEventHandler>();
        }


        public static void ConfigureEventBusKafka(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // đăng ký xử lý
            eventBus.Subscribe<CreateHistoryIntegrationEvent, CreateHistoryIntegrationEventHandler>();
        }
    }
}