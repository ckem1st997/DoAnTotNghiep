using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create.Vendor
{
    public class CreateUnitCommandHandler: IRequestHandler<CreateUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.BeginningWareHouse> _repository;
        private readonly IMapper _mapper;

        public CreateUnitCommandHandler(IRepositoryEF<Domain.Entity.BeginningWareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateBeginningWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.BeginningWareHouse>(request.BeginningWareHouseCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}