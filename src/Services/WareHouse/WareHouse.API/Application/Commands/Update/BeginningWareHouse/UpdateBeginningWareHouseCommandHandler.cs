using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public class UpdateBeginningWareHouseCommandHandler : IRequestHandler<UpdateBeginningWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.BeginningWareHouse> _repository;
        private readonly IMapper _mapper;

        public UpdateBeginningWareHouseCommandHandler(IRepositoryEF<Domain.Entity.BeginningWareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateBeginningWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.BeginningWareHouse>(request.BeginningWareHouseCommands);
            result.ModifiedDate=DateTime.Now;
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}