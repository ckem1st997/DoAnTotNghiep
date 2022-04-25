using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Serilog.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KafKa.Net
{
    public class KafKaConnection : IKafKaConnection
    {
        private readonly ILogger<KafKaConnection> _logger;
        private readonly int _retryCount;
        protected ConcurrentDictionary<string, Lazy<IConsumer<string, byte[]>>> Consumers { get; }
        protected ConcurrentDictionary<string, Lazy<IProducer<string, byte[]>>> Producers { get; }

      //  private IConsumer<string, byte[]> kafkaConsumer;
      //  private IProducer<string, byte[]> kafkaProducer;
        private readonly IConfiguration _configuration;
        bool _disposed;

        object sync_root = new object();

        public string Topic { get; set; }
        private string GroupId { get; set; }
        private string BootstrapServers { get; set; }
        private string connectionName { get; set; }

        public KafKaConnection(ILogger<KafKaConnection> logger, IConfiguration configuration, int retryCount = 5)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _retryCount = retryCount;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Topic = _configuration["KafKaTopic"];
            GroupId = _configuration["KafKaGroupId"];
            BootstrapServers = _configuration["KafKaBootstrapServers"];
            connectionName = _configuration["KafKaconnectionName"];
            Consumers = new ConcurrentDictionary<string, Lazy<IConsumer<string, byte[]>>>();
            Producers = new ConcurrentDictionary<string, Lazy<IProducer<string, byte[]>>>();

        }

        private IProducer<string, byte[]> ProducerConfigMethod()
        {
            return Producers.GetOrAdd(
               connectionName, connection => new Lazy<IProducer<string, byte[]>>(() =>
               {
                   var pConfig = new ProducerConfig()
                   {
                       BootstrapServers = BootstrapServers,
                       ClientId = Dns.GetHostName(),

                   };
                   return new ProducerBuilder<string, byte[]>(pConfig).Build();
               })
           ).Value;
            //this.kafkaProducer = new ProducerBuilder<string, byte[]>(pConfig).Build();
            //return kafkaProducer;
        }

        private IConsumer<string, byte[]> ConsumerConfigMethod()
        {
            return Consumers.GetOrAdd(
                connectionName, connection => new Lazy<IConsumer<string, byte[]>>(() =>
                {
                    var consumerConfig = new ConsumerConfig()
                    {
                        BootstrapServers = BootstrapServers,
                        GroupId = GroupId,
                        AllowAutoCreateTopics = true,
                        EnableAutoCommit = true,
                        EnableAutoOffsetStore = false
                    };

                    return new ConsumerBuilder<string, byte[]>(consumerConfig).Build();
                })
            ).Value;
        }

        public bool IsConnected
        {
            get
            {
                return ProducerConfig != null || ConsumerConfig != null && !_disposed;
            }
        }

        public IProducer<string, byte[]> ProducerConfig
        {
            get
            {
                return this.ProducerConfigMethod();
            }
        }


        public IConsumer<string, byte[]> ConsumerConfig
        {
            get
            {
                return this.ConsumerConfigMethod();
            }
        }

        public bool TryConnect()
        {
            _logger.LogInformation("Kafka Client is trying to connect");
            lock (sync_root)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                    .Or<KafkaRetriableException>()
                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                        _logger.LogWarning(ex, "Kafka Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                    }
                );

                policy.Execute(() =>
                {
                    if (ProducerConfig == null)
                        this.ProducerConfigMethod();
                    if (ConsumerConfig == null)
                        this.ConsumerConfigMethod();
                });

                if (IsConnected)
                {
                    _logger.LogInformation("Kafka Client acquired a persistent connection to '{HostName}' and is subscribed to failure events", Dns.GetHostName());
                    return true;
                }
                else
                {
                    _logger.LogCritical("FATAL ERROR: Kafka connections could not be created and opened");
                    return false;
                }
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;


            if (!Consumers.Any())
            {
                //  Logger.LogDebug($"Disposed consumer pool with no consumers in the pool.");
                return;
            }

            foreach (var consumer in Consumers.Values)
            {

                try
                {
                    consumer.Value.Close();
                    consumer.Value.Dispose();
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex.Message.ToString());
                }

            }
            if (!Producers.Any())
            {
                return;
            }

            foreach (var consumer in Producers.Values)
            {

                try
                {
                    consumer.Value.Dispose();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message.ToString());
                }

            }

            Consumers.Clear();
            Producers.Clear();
        }


    }
}
