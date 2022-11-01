using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateAuditCommandHandler : IRequestHandler<UpdateAuditCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Audit> _repository;
        private readonly IMapper _mapper;

        public UpdateAuditCommandHandler(IRepositoryEF<Domain.Entity.Audit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAuditCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.Audit>(request.AuditCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}