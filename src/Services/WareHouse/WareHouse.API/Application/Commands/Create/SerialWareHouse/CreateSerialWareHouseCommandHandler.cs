using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateSerialWareHouseCommandHandler: IRequestHandler<UpdateSerialWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.SerialWareHouse> _repository;
        private readonly IMapper _mapper;

        public UpdateSerialWareHouseCommandHandler(IRepositoryEF<Domain.Entity.SerialWareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateSerialWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.SerialWareHouse>(request.SerialWareHouseCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}