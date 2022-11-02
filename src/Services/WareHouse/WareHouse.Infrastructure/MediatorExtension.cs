
using System;
using MediatR;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WareHouse.Domain.Entity;
using Share.BaseCore;

namespace WareHouse.Infrastructure
{
    static class MediatorExtension
    {
        /// <summary>
        /// Hàm dùng để Publish các sự kiện thông báo ví dụ như gửi email
        /// các sự kiện không có gửi response lại cho người dùng mà chỉ đơn giản thực hiện một tác vụ
        /// thay vì bạn phải viết await _mediat.Publish(new CreateProductDomainEvent(mode));
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="ctx"></param>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, WarehouseManagementContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
                Console.WriteLine($"\n------\nA domain event has been published!\n" +
                                  $"Event: {domainEvent.GetType().Name}\n" +
                                  $"Data: {JsonConvert.SerializeObject(domainEvent)}\n------\n");
            }

        }
    }
}
