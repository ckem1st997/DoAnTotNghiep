using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWarehouseBalanceCommandHandler: IRequestHandler<UpdateWarehouseBalanceCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WarehouseBalance> _repository;
        private readonly IMapper _mapper;

        public UpdateWarehouseBalanceCommandHandler(IRepositoryEF<Domain.Entity.WarehouseBalance> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWarehouseBalanceCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WarehouseBalance>(request.WarehouseBalanceCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}