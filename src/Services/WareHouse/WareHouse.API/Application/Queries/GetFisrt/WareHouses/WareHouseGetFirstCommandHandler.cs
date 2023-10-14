using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Share.Base.Service;
using Share.Base.Service.Repository;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetFisrt.WareHouses
{
    public class WareHouseGetFirstCommand : Share.Base.Core.Infrastructure.BaseModel, IRequest<WareHouseDTO>
    {
    }

    public class WareHouseGetFirstCommandHandler : IRequestHandler<WareHouseGetFirstCommand, WareHouseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;

        public WareHouseGetFirstCommandHandler(IMapper mapper)
        {
            _repository = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse) ?? throw new ArgumentNullException(nameof(_repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WareHouseDTO> Handle(WareHouseGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetByIdsync(request.Id, cancellationToken);
            return _mapper.Map<WareHouseDTO>(res);
        }
    }
}