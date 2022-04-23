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
        private readonly string topic;
        private readonly IConsumer<string, byte[]> kafkaConsumer;
        private string Topic = "WareHouse-KafKa";
        public RequestTimeConsumer()
        {
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "warehouse",
                AllowAutoCreateTopics = true,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                EnablePartitionEof = true,
                // A good introduction to the CooperativeSticky assignor and incremental rebalancing:
                // https://www.confluent.io/blog/cooperative-rebalancing-in-kafka-streams-consumer-ksqldb/
             //   PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky                
            };
            this.topic = Topic;
            this.kafkaConsumer = new ConsumerBuilder<string, byte[]>(consumerConfig).Build();
        }
       public  class ttt
        {
            public string name { get; set; }
            public string age { get; set; }
        }
        public void tets()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName(),
            };

           
        //    for (int i = 0; i < 1; i++)
         //   {
                var model = new ttt
                {
                    name = "hợp",
                    age = DateTime.Now.ToString()
                };
                using (var producer = new ProducerBuilder<string, byte[]>(config).Build())
                {
                    var body = JsonSerializer.SerializeToUtf8Bytes(model, model.GetType(), new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
                    producer.Produce(this.topic, new Message<string, byte[]> { Key = model.GetType().ToString(), Value = body });

                    Console.WriteLine($"Wrote to offset: ");
                    producer.Flush(timeout: TimeSpan.FromSeconds(10));
                }
          //  }
          
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            tets();

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
                    if(cr.Message !=null)
                    {
                        var message = Encoding.UTF8.GetString(cr.Value);
                        Console.WriteLine($"{cr.Message.Key}: {cr.Message.Value}ms");
                        Console.WriteLine(message);                     
                    }
                }
                catch (OperationCanceledException)
                {
                  //  this.kafkaConsumer.Close();
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");
                  //  this.kafkaConsumer.Close();
                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
                 //   this.kafkaConsumer.Close();
                    break;

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