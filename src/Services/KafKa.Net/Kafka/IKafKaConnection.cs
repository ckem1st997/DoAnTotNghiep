using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafKa.Net
{
    public  interface IKafKaConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IProducer<string, byte[]> ProducerConfig { get; }
        IConsumer<string, byte[]> ConsumerConfig { get; }
    }
}