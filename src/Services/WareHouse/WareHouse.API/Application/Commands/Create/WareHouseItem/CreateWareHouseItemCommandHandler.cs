using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseItemCommandHandler: IRequestHandler<CreateWareHouseItemCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItem> _repository;
        private readonly IMapper _mapper;

        public CreateWareHouseItemCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItem> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateWareHouseItemCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouseItem>(request.WareHouseItemCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}