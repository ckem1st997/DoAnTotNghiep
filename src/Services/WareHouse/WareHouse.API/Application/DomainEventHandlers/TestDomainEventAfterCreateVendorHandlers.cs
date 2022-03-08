using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using WareHouse.Domain;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application
{
    public partial class
        TestDomainEventAfterCreateVendorHandlers : INotificationHandler<TestEditDomainEventAfterCreateVendor>
    {
        private readonly IRepositoryEF<Domain.Entity.Vendor> _repository;

        public TestDomainEventAfterCreateVendorHandlers(
            IRepositoryEF<Domain.Entity.Vendor> repository)
        {
            _repository = repository;
        }

        public async Task Handle(TestEditDomainEventAfterCreateVendor domainEvent, CancellationToken cancellationToken)
        {
            // MessageResponse message = new MessageResponse();
            if (domainEvent == null || string.IsNullOrEmpty(domainEvent._TextEditToEntity) ||
                string.IsNullOrEmpty(domainEvent._Id))
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            var GetEqualityComponents = await _repository.GetFirstAsync(domainEvent._Id);
            if (GetEqualityComponents is null)
                throw new ArgumentNullException(nameof(GetEqualityComponents));
            GetEqualityComponents.Address = domainEvent._TextEditToEntity;
            _repository.Update(GetEqualityComponents);
            await _repository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}