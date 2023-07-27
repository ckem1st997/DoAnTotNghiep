﻿using Autofac;
using Confluent.Kafka;
using KafKa.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using Serilog;
using Share.BaseCore.EventBus;
using Share.BaseCore.EventBus.Abstractions;
using Share.BaseCore.EventBus.Events;
using Share.BaseCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Share.BaseCore.Kafka
{
    public class EventKafKa : IEventBus, IDisposable
    {
        private readonly IKafKaConnection _persistentConnection;
        private readonly IEventBusSubscriptionsManager _subsManager;
        private readonly string _topicName;

        public EventKafKa(IConfiguration configuration, IKafKaConnection persistentConnection, IEventBusSubscriptionsManager subsManager, string queueName = null)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
            //  _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
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
        public bool Publish(IntegrationEvent @event, string Topic = "")
        {
            try
            {
                if (_persistentConnection.IsConnectedProducer)
                {
                    var eventName = @event.GetType().Name;

                    Log.Information($"Creating KafKa Topic by EventBus to publish event: {@event.Id} ({eventName})");
                    var producer = _persistentConnection.ProducerConfig;
                    var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    producer.Produce(Topic ?? _topicName, new Message<string, byte[]> { Key = eventName, Value = body });
                    producer.Flush(timeout: TimeSpan.FromSeconds(3));
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {

                Log.Error(e.Message);
                return false;
            }


        }



        /// <summary>
        /// Xuất bản một tin nhắn.
        /// </summary>
        /// <param name="event"></param>
        public async Task<bool> PublishAsync(IntegrationEvent @event, string Topic = "")
        {
            try
            {
                if (_persistentConnection.IsConnectedProducer)
                {
                    var eventName = @event.GetType().Name;

                    Log.Information($"Creating KafKa Topic by EventBus to publish event: {@event.Id} ({eventName})");
                    var producer = _persistentConnection.ProducerConfig;
                    var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    var res = await producer.ProduceAsync(Topic ?? _topicName, new Message<string, byte[]> { Key = eventName, Value = body });
                    Log.Information($"Message sent (value: {res.Value}), topic: {res.Topic}, partition: {res.Partition}, offset: {res.Offset}");
                    producer.Flush();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return false;
            }


        }


        public void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            Log.Information($"Subscribing to dynamic event {eventName} with {typeof(TH).GetGenericTypeName()}");
            _subsManager.AddDynamicSubscription<TH>(eventName);
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();
            Log.Information($"Subscribing to event {eventName} width {typeof(TH).GetGenericTypeName()}");
            _subsManager.AddSubscription<T, TH>();
        }


        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subsManager.GetEventKey<T>();

            Log.Information($"Unsubscribing from event {eventName}");

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
            //if (_persistentConnection.IsConnectedProducer)
            //    _persistentConnection.TryConnectProducer();
            return _persistentConnection.IsConnectedProducer;
        }
        public bool IsConnectedConsumer()
        {
            //if (_persistentConnection.IsConnectedConsumer)
            //    _persistentConnection.TryConnectConsumer();
            return _persistentConnection.IsConnectedConsumer;
        }

    }
}
