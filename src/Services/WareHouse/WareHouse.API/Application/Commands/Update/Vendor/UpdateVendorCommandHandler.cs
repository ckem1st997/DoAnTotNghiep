using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateVendorCommandHandler: IRequestHandler<UpdateVendorCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Vendor> _repository;
        private readonly IMapper _mapper;

        public UpdateVendorCommandHandler(IRepositoryEF<Domain.Entity.Vendor> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Vendor>(request.VendorCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}