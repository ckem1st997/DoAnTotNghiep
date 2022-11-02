using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Share.BaseCore.IRepositories;
using MediatR;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetFisrt.WareHouses
{
    public class WareHouseGetFirstCommand : Model.BaseModel, IRequest<WareHouseDTO>
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
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<WareHouseDTO>(res);
        }
    }
}