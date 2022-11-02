using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Share.BaseCore.IRepositories;
using MediatR;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseItemUnitCommandHandler: IRequestHandler<CreateWareHouseItemUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemUnit> _repository;
        private readonly IMapper _mapper;

        public CreateWareHouseItemUnitCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemUnit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateWareHouseItemUnitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouseItemUnit>(request.WareHouseItemUnitCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}