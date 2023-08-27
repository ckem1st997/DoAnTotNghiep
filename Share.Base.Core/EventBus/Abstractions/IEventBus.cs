using Share.Base.Core.EventBus.Events;
using System.Threading.Tasks;

namespace Share.Base.Core.EventBus.Abstractions
{
    public interface IEventBus
    {
        /// <summary>
        /// Xuất bản một tin nhắn.
        /// </summary>
        /// <param name="event"></param>
        bool Publish(IntegrationEvent @event, string Topic = "");
        Task<bool> PublishAsync(IntegrationEvent @event, string Topic = "");

        bool IsConnectedConsumer();
        bool IsConnectedProducer();


        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
