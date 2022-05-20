using Autofac;
using KafKa.Net.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafKa.Net.Kafka
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
         //   services.AddTransient<TestIntegrationEventHandler>();
        }


        //public static void ConfigureEventBus(this IApplicationBuilder app)
        //{
        //    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
        //    // đăng ký xử lý
        //    eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
        //}
    }
}