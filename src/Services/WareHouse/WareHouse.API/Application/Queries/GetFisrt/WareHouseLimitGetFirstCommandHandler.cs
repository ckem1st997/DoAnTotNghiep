using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class WareHouseLimitGetFirstCommand : Model.BaseModel, IRequest<WareHouseLimitDTO>
    {
    }

    public class WareHouseLimitGetFirstCommandHandler : IRequestHandler<WareHouseLimitGetFirstCommand, WareHouseLimitDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.WareHouseLimit> _repository;

        public WareHouseLimitGetFirstCommandHandler(IRepositoryEF<Domain.Entity.WareHouseLimit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<WareHouseLimitDTO> Handle(WareHouseLimitGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<WareHouseLimitDTO>(res);
        }
    }
}