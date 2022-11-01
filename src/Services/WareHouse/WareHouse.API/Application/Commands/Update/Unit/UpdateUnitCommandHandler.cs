using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Unit> _repository;
        private readonly IMapper _mapper;

        public UpdateUnitCommandHandler(IRepositoryEF<Domain.Entity.Unit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Unit>(request.UnitCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}