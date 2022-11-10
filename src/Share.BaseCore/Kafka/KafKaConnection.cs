using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using Serilog;
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

namespace Share.BaseCore.Kafka
{
    public class KafKaConnection : IKafKaConnection
    {
        private readonly ILogger<KafKaConnection> _logger;
        private readonly int _retryCount;

        private IConsumer<string, byte[]> kafkaConsumer;
        private IProducer<string, byte[]> kafkaProducer;
        private readonly IConfiguration _configuration;
        private Error Error;
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
            kafkaConsumer = ConsumerConfigMethod();
            kafkaProducer = ProducerConfigMethod();
            Log.Information(BootstrapServers);
        }

        private IProducer<string, byte[]> ProducerConfigMethod()
        {
            var pConfig = new ProducerConfig()
            {
                BootstrapServers = BootstrapServers,
                ClientId = Dns.GetHostName(),
            };

            return new ProducerBuilder<string, byte[]>(pConfig).SetErrorHandler((p, e) =>
            {
                Error = e;
            }).Build();
        }

        private IConsumer<string, byte[]> ConsumerConfigMethod()
        {
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = BootstrapServers,
                GroupId = GroupId,
                AllowAutoCreateTopics = true,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = false,
                HeartbeatIntervalMs = 1000
            };
            return new ConsumerBuilder<string, byte[]>(consumerConfig).SetErrorHandler((p, e) =>
            {
                Error = e;
            }).Build();
        }

        public bool IsConnectedConsumer
        {
            get { return GetErrorCheck(); }
        }

        private bool GetErrorCheck()
        {
            return Error == null || Error != null && !Error.Reason.Contains("failed: Unknown error");
        }

        public bool IsConnectedProducer
        {
            get { return GetErrorCheck(); }
        }

        public IProducer<string, byte[]> ProducerConfig
        {
            get { return kafkaProducer; }
        }


        public IConsumer<string, byte[]> ConsumerConfig
        {
            get { return kafkaConsumer; }
        }

        public bool TryConnectConsumer()
        {
            Log.Information("Kafka Client is trying to connect");
            //  lock (sync_root)
            //  {
            //var policy = RetryPolicy.Handle<SocketException>()
            //    .Or<KafkaRetriableException>()
            //    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            //        (ex, time) =>
            //        {
            //            _logger.LogWarning(ex,
            //                "Kafka Client could not connect after {TimeOut}s ({ExceptionMessage})",
            //                $"{time.TotalSeconds:n1}", ex.Message);
            //        }
            //    );

            //  policy.Execute(() =>
            //  {
            if (ConsumerConfig == null)
                kafkaConsumer = ConsumerConfigMethod();
            //  });

            if (IsConnectedConsumer)
            {
                Log.Information(
                    "Kafka Client acquired a persistent connection to '{HostName}' and is subscribed to failure events",
                    Dns.GetHostName());
                return true;
            }
            else
            {
                Log.Error("FATAL ERROR: Kafka connections could not be created and opened");
                return false;
            }
            //  }
        }


        public bool TryConnectProducer()
        {
            Log.Information("Kafka Client is trying to connect");
            //  lock (sync_root)
            //   {
            //var policy = RetryPolicy.Handle<SocketException>()
            //    .Or<KafkaRetriableException>()
            //    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
            //        (ex, time) =>
            //        {
            //            _logger.LogWarning(ex,
            //                "Kafka Client could not connect after {TimeOut}s ({ExceptionMessage})",
            //                $"{time.TotalSeconds:n1}", ex.Message);
            //        }
            //    );

            //policy.Execute(() =>
            //{
            if (ProducerConfig == null)
                kafkaProducer = ProducerConfigMethod();
            // });

            if (IsConnectedProducer)
            {
                Log.Information(
                    "Kafka Client acquired a persistent connection to '{HostName}' and is subscribed to failure events",
                    Dns.GetHostName());
                return true;
            }
            else
            {
                Log.Error("FATAL ERROR: Kafka connections could not be created and opened");
                return false;
            }
            //  }
        }
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                kafkaConsumer.Close();
                kafkaProducer.Flush();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
        }

    }
}