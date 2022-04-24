using System;
using Confluent.Kafka;

namespace KafKa.Net;

public interface IConsumerPool : IDisposable
{
    IConsumer<string, byte[]> Get(string groupId, string connectionName = null);
}
