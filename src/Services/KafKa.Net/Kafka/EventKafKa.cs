using Autofac;
using Confluent.Kafka;
using KafKa.Net.Abstractions;
using KafKa.Net.Events;
using KafKa.Net.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace KafKa.Net.Kafka
{
    public class EventKafKa : IEventBus, IDisposable
    {
        const string BROKER_NAME = "event_bus";
        const string AUTOFAC_SCOPE_NAME = "event_bus";

        private readonly IKafKaConnection _persistentConnection;
        private readonly ILogger<EventKafKa> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly string _topicName;

        public EventKafKa(IConfiguration configuration, IKafKaConnection persistentConnection, ILogger<EventKafKa> logger, IEventBusSubscriptionsManager subsManager, string queueName = null)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
            _topicName = configuration["KafKaTopic"];
        }



        /// <summary>
        /// bỏ liên kết một hàng đợi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventName"></param>
        private void SubsManager_OnEventRemoved(object sender, string eventName)
        {
            if (!_persistentConnection.IsConnectedProducer)
            {
                _persistentConnection.TryConnectProducer();
            }
            if (!_persistentConnection.IsConnectedConsumer)
            {
                _persistentConnection.TryConnectConsumer();
            }
        }


        /// <summary>
        /// Xuất bản một tin nhắn.
        /// </summary>
        /// <param name="event"></param>
        public void Publish(IntegrationEvent @event)
        {
            if (_persistentConnection.IsConnectedProducer)
            {
                var eventName = @event.GetType().Name;

                _logger.LogTrace("Creating KafKa Topic to publish event: {EventId} ({EventName})", @event.Id, eventName);
                var producer = _persistentConnection.ProducerConfig;
                var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                producer.Produce(_topicName, new Message<string, byte[]> { Key = eventName, Value = body });
                producer.Flush(timeout: TimeSpan.FromSeconds(5));
            }

        }

        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _logger.LogInformation("Subscribing to dynamic event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());
            _subsManager.AddSubscription<T, TH>();
        }


        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        public void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            _subsManager.RemoveDynamicSubscription<TH>(eventName);
        }

        public void Dispose()
        {
            if (_persistentConnection != null)
            {
                _persistentConnection.Dispose();
            }

            _subsManager.Clear();
        }

        public bool IsConnectedProducer()
        {
            if (_persistentConnection.IsConnectedProducer)
                _persistentConnection.TryConnectProducer();
            return _persistentConnection.IsConnectedProducer;
        }
        public bool IsConnectedConsumer()
        {
            if (_persistentConnection.IsConnectedConsumer)
                _persistentConnection.TryConnectConsumer();
            return _persistentConnection.IsConnectedConsumer;
        }

    }
}
