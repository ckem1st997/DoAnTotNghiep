using System;
using Confluent.Kafka;

namespace KafKa.Net;

public interface IProducerPool : IDisposable
{
    IProducer<string, byte[]> Get(string connectionName = null);
}
