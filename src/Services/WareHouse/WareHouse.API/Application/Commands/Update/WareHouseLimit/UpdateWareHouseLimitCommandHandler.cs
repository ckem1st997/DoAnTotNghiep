using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWareHouseLimitCommandHandler : IRequestHandler<UpdateWareHouseLimitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseLimit> _repository;
        private readonly IMapper _mapper;

        public UpdateWareHouseLimitCommandHandler(IRepositoryEF<Domain.Entity.WareHouseLimit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateWareHouseLimitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.WareHouseLimit>(request.WareHouseLimitCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}