using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Vendor> _repository;
        private readonly IMapper _mapper;

        public CreateVendorCommandHandler(IRepositoryEF<Domain.Entity.Vendor> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Vendor>(request.VendorCommands);
            var res = await _repository.AddAsync(result);
            // var TestEditDomainEventAfterCreateVendor =
            //     new TestEditDomainEventAfterCreateVendor("test nhe 1111111", res.Id);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken)>0;
        }
    }
}