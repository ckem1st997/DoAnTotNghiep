using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Kafka
{
    /// <summary>
    /// get Producer and  Consumer
    /// IKafKaConnection : IDisposable
    /// </summary>
    public interface IKafKaConnection
    {
        bool IsConnectedConsumer { get; }
        bool IsConnectedProducer { get; }

        bool TryConnectConsumer();
        bool TryConnectProducer();

        /// <summary>
        /// get Producer
        /// </summary>
        IProducer<string, byte[]> ProducerConfig { get; }
        /// <summary>
        /// get Consumer
        /// </summary>
        IConsumer<string, byte[]> ConsumerConfig { get; }
    }
}