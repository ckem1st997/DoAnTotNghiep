using Autofac;
using KafKa.Net;
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
using WareHouse.API.IntegrationEvents.EventHandling;

namespace WareHouse.API.IntegrationEvents
{
    public static class EventBusKafKa
    {
        public static void AddEventBusKafka(this IServiceCollection services, IConfiguration configuration)
        {
            // thêm xử lí event trong app
            //services.AddTransient<InwardIntegrationEventHandler>();
        }


        public static void ConfigureEventBusKafka(this IApplicationBuilder app)
        {
          //  var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // đăng ký xử lý
            //  eventBus.Subscribe<InwardIntegrationEvent, InwardIntegrationEventHandler>();
        }
    }
}