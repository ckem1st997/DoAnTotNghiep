using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateWareHouseItemUnitCommandHandler: IRequestHandler<UpdateWareHouseItemUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemUnit> _repository;
        private readonly IMapper _mapper;

        public UpdateWareHouseItemUnitCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemUnit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWareHouseItemUnitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouseItemUnit>(request.WareHouseItemUnitCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}