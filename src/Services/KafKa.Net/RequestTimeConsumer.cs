using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using System.Net;

namespace KafKa.Net
{
    /// <summary>
    ///     A simple example demonstrating how to set up a Kafka consumer as an
    ///     IHostedService.
    /// </summary>
    public class RequestTimeConsumer : BackgroundService
    {
        private readonly string topic;
        private readonly IConsumer<string, long> kafkaConsumer;
        private string Topic = "WareHouse-KafKa";
        public RequestTimeConsumer()
        {
            var consumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "warehouse",
                AllowAutoCreateTopics = true,
             //   SaslUsername = "admin",
            //    SaslPassword = "admin",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                //SecurityProtocol = SecurityProtocol.SaslSsl,
                //SaslMechanism= SaslMechanism.Plain,
            };
            this.topic = Topic;
            this.kafkaConsumer = new ConsumerBuilder<string, long>(consumerConfig).Build();
            tets();
        }
        public void tets()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName(),
            };


            using (var producer = new ProducerBuilder<string, string>(config).Build())
            {
                producer.Produce(this.topic, new Message<string, string> { Key="dsadsa",Value = "hello world" });

                Console.WriteLine($"Wrote to offset: ");
                producer.Flush(timeout: TimeSpan.FromSeconds(10));
            }
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

                    // Handle message...
                    Console.WriteLine($"{cr.Message.Key}: {cr.Message.Value}ms");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    // Consumer errors should generally be ignored (or logged) unless fatal.
                    Console.WriteLine($"Consume error: {e.Error.Reason}");

                    if (e.Error.IsFatal)
                    {
                        // https://github.com/edenhill/librdkafka/blob/master/INTRODUCTION.md#fatal-consumer-errors
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Unexpected error: {e}");
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