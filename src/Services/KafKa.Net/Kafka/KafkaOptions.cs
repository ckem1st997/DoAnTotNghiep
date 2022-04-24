using System;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace KafKa.Net;

public class KafkaOptions
{
    public KafkaConnections Connections { get; }

    public Action<ProducerConfig> ConfigureProducer { get; set; }

    public Action<ConsumerConfig> ConfigureConsumer { get; set; }

    public Action<TopicSpecification> ConfigureTopic { get; set; }

    public KafkaOptions()
    {
        Connections = new KafkaConnections();
    }
}
