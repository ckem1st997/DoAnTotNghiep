using Autofac;
using Master.IntegrationEvents;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Share.Base.Core.EventBus;
using Share.Base.Core.EventBus.Abstractions;
using Share.Base.Core.Kafka;
using Share.Base.Service.IntegrationEvents.Events;
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
            // đăng ký hàm xử lý mesage model
            services.AddTransient<CreateHistoryIntegrationEventHandler>();
        }


        public static void ConfigureEventBusKafka(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // đăng ký xử lý
            // hàm xử lý tương ứng model mesage nhận được
            eventBus.Subscribe<CreateHistoryIntegrationEvent, CreateHistoryIntegrationEventHandler>();
        }
    }
}