using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateAuditCouncilCommandHandler: IRequestHandler<UpdateAuditCouncilCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.AuditCouncil> _repository;
        private readonly IMapper _mapper;

        public UpdateAuditCouncilCommandHandler(IRepositoryEF<Domain.Entity.AuditCouncil> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAuditCouncilCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.AuditCouncil>(request.AuditCouncilCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}