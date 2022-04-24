using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Confluent.Kafka;

namespace KafKa.Net;

[Serializable]
public class KafkaConnections : Dictionary<string, ClientConfig>
{
    public const string DefaultConnectionName = "Default";

    [NotNull]
    public ClientConfig Default {
        get => this[DefaultConnectionName];
        set => this[DefaultConnectionName] = value;
    }
    
    public KafkaConnections()
    {
        Default = new ClientConfig();
    }

    public ClientConfig GetOrDefault(string connectionName)
    {
        if (TryGetValue(connectionName, out var connectionFactory))
        {
            return connectionFactory;
        }

        return Default;
    }
}
