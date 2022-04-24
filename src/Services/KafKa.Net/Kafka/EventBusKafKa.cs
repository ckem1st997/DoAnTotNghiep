using Autofac;
using KafKa.Net.Abstractions;
using KafKa.Net.IntegrationEvents.EventHandling;
using KafKa.Net.IntegrationEvents.Events;
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
                var rabbitMQPersistentConnection = sp.GetRequiredService<IKafKaConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventKafKa>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;

                return new EventKafKa(configuration,rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            }
            );

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            // thêm xử lí event trong app
            services.AddTransient<TestIntegrationEventHandler>();
        }


        public static void ConfigureEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            // đăng ký xử lý
            eventBus.Subscribe<TestIntegrationEvent, TestIntegrationEventHandler>();
        }
    }
}