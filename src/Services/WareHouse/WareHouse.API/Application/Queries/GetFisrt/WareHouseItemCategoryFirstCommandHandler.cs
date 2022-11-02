using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class WareHouseItemCategoryFirstCommand : Model.BaseModel, IRequest<WareHouseItemCategoryDTO>
    {
    }
    public class WareHouseItemCategoryFirstCommandHandler : IRequestHandler<WareHouseItemCategoryFirstCommand, WareHouseItemCategoryDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemCategory> _repository;

        public WareHouseItemCategoryFirstCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemCategory> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WareHouseItemCategoryDTO> Handle(WareHouseItemCategoryFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<WareHouseItemCategoryDTO>(res);
        }
    }
}