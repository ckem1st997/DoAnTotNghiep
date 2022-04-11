using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Threading;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetFisrt
{
    public class UnitGetFirstCommand : Model.BaseModel, IRequest<UnitDTO>
    {
    }

    public class UnitGetFirstCommandHandler : IRequestHandler<UnitGetFirstCommand, UnitDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryEF<Domain.Entity.Unit> _repository;

        public UnitGetFirstCommandHandler(IRepositoryEF<Domain.Entity.Unit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UnitDTO> Handle(UnitGetFirstCommand request, CancellationToken cancellationToken)
        {
            if (request?.Id is null)
                return null;
            var res = await _repository.GetFirstAsyncAsNoTracking(request.Id);
            return _mapper.Map<UnitDTO>(res);
        }
    }
}