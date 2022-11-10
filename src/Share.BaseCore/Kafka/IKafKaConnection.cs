using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Kafka
{
    public interface IKafKaConnection : IDisposable
    {
        bool IsConnectedConsumer { get; }
        bool IsConnectedProducer { get; }

        bool TryConnectConsumer();
        bool TryConnectProducer();

        IProducer<string, byte[]> ProducerConfig { get; }
        IConsumer<string, byte[]> ConsumerConfig { get; }
    }
}