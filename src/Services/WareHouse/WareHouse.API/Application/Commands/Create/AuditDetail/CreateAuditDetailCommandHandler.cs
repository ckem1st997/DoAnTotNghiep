using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateAuditDetailCommandHandler: IRequestHandler<CreateAuditDetailCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.AuditDetail> _repository;
        private readonly IMapper _mapper;

        public CreateAuditDetailCommandHandler(IRepositoryEF<Domain.Entity.AuditDetail> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateAuditDetailCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.AuditDetail>(request.AuditDetailCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}