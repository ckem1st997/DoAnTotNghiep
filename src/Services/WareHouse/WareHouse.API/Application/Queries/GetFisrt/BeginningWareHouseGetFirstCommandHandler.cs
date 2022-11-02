using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetFisrt.WareHouses
{
    public class BeginningWareHouseGetFirstCommand : Model.BaseModel, IRequest<BeginningWareHouseDTO>
    {
    }

    public class BeginningWareHouseGetFirstCommandHandler : IRequestHandler<BeginningWareHouseGetFirstCommand, BeginningWareHouseDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.BeginningWareHouse> _repository;

        public BeginningWareHouseGetFirstCommandHandler(IRepositoryEF<Domain.Entity.BeginningWareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<BeginningWareHouseDTO> Handle(BeginningWareHouseGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<BeginningWareHouseDTO>(res);
        }
    }
}