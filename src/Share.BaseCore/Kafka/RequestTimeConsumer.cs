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
using Autofac;
using Polly.Retry;
using System.Net.Sockets;
using Polly;
using Serilog;
using Share.BaseCore.EventBus.Abstractions;
using Share.BaseCore.EventBus;

namespace Share.BaseCore.Kafka
{
    /// <summary>
    ///     A simple example demonstrating how to set up a Kafka consumer as an
    ///     IHostedService.
    /// </summary>
    public sealed class RequestTimeConsumer : BackgroundService
    {
        private readonly IKafKaConnection _kafKaConnection;
        private readonly string topic;
        private readonly IConsumer<string, byte[]> kafkaConsumer;
        private readonly string Topic = "WareHouse-KafKa";
        private readonly ILogger<EventKafKa> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly ILifetimeScope _autofac;
        const string BROKER_NAME = "event_bus";
        const string AUTOFAC_SCOPE_NAME = "event_bus";

        public RequestTimeConsumer(IKafKaConnection kafKaConnection, ILogger<EventKafKa> logger,
            ILifetimeScope autofac, IEventBusSubscriptionsManager subsManager)
        {
            topic = Topic;
            _kafKaConnection = kafKaConnection;
            kafkaConsumer = _kafKaConnection.ConsumerConfig;
            _autofac = autofac;
            _subsManager = subsManager;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var policy = Policy.Handle<SocketException>()
            //    .Or<KafkaRetriableException>()
            //    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            //        (ex, time) =>
            //        {
            //            Log.Warning(ex, "Kafka Client could not connect after {TimeOut}s ({ExceptionMessage})",
            //                $"{time.TotalSeconds:n1}", ex.Message);
            //        }
            //    );

            //await policy.ExecuteAsync(() =>
            //{
            //    return StartConsumerLoop(stoppingToken);
            //}
            //);
            return StartConsumerLoop(stoppingToken);

            // return Task.CompletedTask;
        }

        // [Obsolete]
        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            int delay = 1;
            while (!cancellationToken.IsCancellationRequested)
            {
                Log.Information("Listen to Kafka");
                if (!_kafKaConnection.IsConnectedConsumer)
                {
                    Log.Error("Kafka Client is not connected");
                    _kafKaConnection.TryConnectConsumer();
                }
                else
                {
                    kafkaConsumer.Subscribe(topic);

                    try
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationToken);
                        //await Task.CompletedTask;
                        if (delay > 0)
                        {
                            Console.WriteLine(delay);
                            delay /= 2;

                        }

                        var cr = kafkaConsumer.Consume(cancellationToken);
                        if (cr.Message != null)
                        {
                            var message = Encoding.UTF8.GetString(cr.Value);
                            await ProcessEvent(cr.Key, message);
                        }

                        kafkaConsumer.StoreOffset(cr);
                    }
                    catch (ConsumeException e)
                    {
                        Log.Error($"Consume error: {e.Error.Code}");
                        kafkaConsumer.Close();
                        if (e.Error.IsFatal)
                        {
                            break;
                        }
                    }
                }


            }
            kafkaConsumer.Close();

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
                            IDynamicIntegrationEventHandler handler =
                                scope.ResolveOptional(subscription.HandlerType) as IDynamicIntegrationEventHandler;
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
                            var integrationEvent = JsonSerializer.Deserialize(message, eventType,
                                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                            var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                            await Task.Yield();
                            await (Task)concreteType.GetMethod("Handle")
                                .Invoke(handler, new object[] { integrationEvent });
                        }
                    }
                }
            }
            else
            {
                Log.Warning("No subscription for KafKa event: {EventName}", eventName);
            }
        }
        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            Log.Information($"{nameof(RequestTimeConsumer)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
        //public override void Dispose()
        //{
        //    kafkaConsumer?.Close(); // Commit offsets and leave the group cleanly.
        //    kafkaConsumer?.Dispose();
        //    base.Dispose();
        //}
    }
}