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
        public RequestTimeConsumer(IKafKaConnection kafKaConnection)
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
            this.kafkaConsumer = new ConsumerBuilder<string, byte[]>(consumerConfig).Build();
            _kafKaConnection = kafKaConnection;
        }
        public class ttt
        {
            public string name { get; set; }
            public string age { get; set; }
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            new Thread(() => StartConsumerLoop(stoppingToken)).Start();
            Console.WriteLine("Connect");
            return Task.CompletedTask;
        }

        private void StartConsumerLoop(CancellationToken cancellationToken)
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
                     this.kafkaConsumer.Close();
                }
             

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