using Autofac;
using KafKa.Net;
using KafKa.Net.Abstractions;
using KafKa.Net.IntegrationEvents;
using KafKa.Net.IntegrationEvents.EventHandling;
using KafKa.Net.IntegrationEvents.Events;
using KafKa.Net.Kafka;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.API.IntegrationEvents
{
    public static class EventBusKafKa
    {
        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {



            services.AddSingleton<IEventBus, EventKafKa>(
                sp =>
            {
                var subscriptionClientName = configuration["SubscriptionClientName"];
                var kafkaPersistentConnection = sp.GetRequiredService<IKafKaConnection>();
                var logger = sp.GetRequiredService<ILogger<EventKafKa>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                return new EventKafKa(configuration, kafkaPersistentConnection, logger, eventBusSubcriptionsManager, subscriptionClientName);
            }
            );

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            // thêm xử lí event trong app
      }


        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
       }
    }
}