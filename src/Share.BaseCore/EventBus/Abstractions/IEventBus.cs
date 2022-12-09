using Share.BaseCore.EventBus.Events;
using System.Threading.Tasks;

namespace Share.BaseCore.EventBus.Abstractions
{
    public interface IEventBus
    {
        /// <summary>
        /// Xuất bản một tin nhắn.
        /// </summary>
        /// <param name="event"></param>
        bool Publish(IntegrationEvent @event);
        Task<bool> PublishAsync(IntegrationEvent @event);

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
