using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateAuditCommandHandler: IRequestHandler<CreateAuditCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Audit> _repository;
        private readonly IMapper _mapper;

        public CreateAuditCommandHandler(IRepositoryEF<Domain.Entity.Audit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateAuditCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Audit>(request.AuditCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}