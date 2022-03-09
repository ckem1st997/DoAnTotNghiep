using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt.WareHouses
{
    public class WareHouseItemFirstCommand : Model.BaseModel, IRequest<WareHouseItemDTO>
    {
    }
    public class WareHouseItemFirstCommandHandler : IRequestHandler<WareHouseItemFirstCommand, WareHouseItemDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.WareHouseItem> _repository;

        public WareHouseItemFirstCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItem> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WareHouseItemDTO> Handle(WareHouseItemFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<WareHouseItemDTO>(res);
        }
    }
}