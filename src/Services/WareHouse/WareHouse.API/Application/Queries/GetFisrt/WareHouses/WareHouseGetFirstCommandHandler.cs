using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
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

        public WareHouseGetFirstCommandHandler(IRepositoryEF<Domain.Entity.WareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WareHouseDTO> Handle(WareHouseGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetByIdsync(request.Id);
            return _mapper.Map<WareHouseDTO>(res);
        }
    }
}