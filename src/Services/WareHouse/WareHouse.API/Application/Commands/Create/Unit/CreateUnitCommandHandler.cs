using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Share.BaseCore.IRepositories;
using MediatR;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateUnitCommandHandler: IRequestHandler<CreateUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Unit> _repository;
        private readonly IMapper _mapper;

        public CreateUnitCommandHandler(IRepositoryEF<Domain.Entity.Unit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Unit>(request.UnitCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}