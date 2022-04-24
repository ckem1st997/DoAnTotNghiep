using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using KafKa.Net.Kafka;
using Autofac;
using KafKa.Net.Abstractions;

namespace KafKa.Net
{
    /// <summary>
    ///     A simple example demonstrating how to set up a Kafka consumer as an
    ///     IHostedService.
    /// </summary>
    public class RequestTimeConsumer : BackgroundService
    {
        private readonly IKafKaConnection _kafKaConnection;
        private readonly string topic;
        private readonly IConsumer<string, byte[]> kafkaConsumer;
        private string Topic = "WareHouse-KafKa";
        private readonly ILogger<EventKafKa> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        const string BROKER_NAME = "event_bus";
        const string AUTOFAC_SCOPE_NAME = "event_bus";
        public RequestTimeConsumer(IKafKaConnection kafKaConnection, ILogger<EventKafKa> logger,
            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager)
        {
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "warehouse",
                AllowAutoCreateTopics = true,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = false             
            };
            this.topic = Topic;
          //  this.kafkaConsumer = new ConsumerBuilder<string, byte[]>(consumerConfig).Build();
            _kafKaConnection = kafKaConnection;
            this.kafkaConsumer = this._kafKaConnection.ConsumerConfig;
            _autofac = autofac;
            _subsManager = subsManager;
            _logger = logger;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();
            return Task.CompletedTask;
        }

        private async void StartConsumerLoop(CancellationToken cancellationToken)
        {
            kafkaConsumer.Subscribe(this.topic);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var cr = this.kafkaConsumer.Consume(cancellationToken);
                    if (cr.Message != null)
                    {
                        var message = Encoding.UTF8.GetString(cr.Value);
                        Console.WriteLine(message);
                        await ProcessEvent(cr.Key, message);

                    }

                    kafkaConsumer.StoreOffset(cr);
                  
                    // try
                    // {
                    //     this.kafkaConsumer.Commit(cr);
                    // }
                    // catch (KafkaException e)
                    // {
                    //     Console.WriteLine($"Commit error: {e.Error.Reason}");
                    // }
                    //  }
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");
                    //  this.kafkaConsumer.Close();
                    if (e.Error.IsFatal)
                    {
                        break;
                    }
                }
                finally
                {
                  //   this.kafkaConsumer.Close();
                }            
            }
            this.kafkaConsumer.Close();
        }
        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing KafKa event: {EventName}", eventName);

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                using (var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME))
                {
                    var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        if (subscription.IsDynamic)
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
                            if (handler == null) continue;
                            using dynamic eventData = JsonDocument.Parse(message);
                            await Task.Yield();
                            await handler.Handle(eventData);
                        }
                        else
                        {
                            var handler = scope.ResolveOptional(subscription.HandlerType);
                            if (handler == null) continue;
                            var eventType = _subsManager.GetEventTypeByName(eventName);
                            var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await Task.Yield();
                            await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for KafKa event: {EventName}", eventName);
            }
        }

        public override void Dispose()
        {
            this.kafkaConsumer.Close(); // Commit offsets and leave the group cleanly.
            this.kafkaConsumer.Dispose();

            base.Dispose();
        }


    }


}