using System;
using Microsoft.Extensions.DependencyInjection;


namespace KafKa.Net;

public class KafkaMessageConsumerFactory 
    //: IKafkaMessageConsumerFactory, IDisposable
{
    //protected IServiceScope ServiceScope { get; }

    //public KafkaMessageConsumerFactory(IServiceScopeFactory serviceScopeFactory)
    //{
    //    ServiceScope = serviceScopeFactory.CreateScope();
    //}

    //public IKafkaMessageConsumer Create(
    //    string topicName,
    //    string groupId,
    //    string connectionName = null)
    //{
    //    var consumer = ServiceScope.ServiceProvider.GetRequiredService<KafkaMessageConsumer>();
    //  //  consumer.Initialize(topicName, groupId, connectionName);
    //    return consumer;
    //}

    //public void Dispose()
    //{
    //    ServiceScope?.Dispose();
    //}
}
