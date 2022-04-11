using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class VendorGetFirstCommand : Model.BaseModel, IRequest<VendorDTO>
    {
    }

    public class VendorGetFirstCommandHandler : IRequestHandler<VendorGetFirstCommand, VendorDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.Vendor> _repository;

        public VendorGetFirstCommandHandler(IRepositoryEF<Domain.Entity.Vendor> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VendorDTO> Handle(VendorGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<VendorDTO>(res);
        }
    }
}