using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateAuditDetailSerialCommandHandler : IRequestHandler<UpdateAuditDetailSerialCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.AuditDetailSerial> _repository;
        private readonly IMapper _mapper;

        public UpdateAuditDetailSerialCommandHandler(IRepositoryEF<Domain.Entity.AuditDetailSerial> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateAuditDetailSerialCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.AuditDetailSerial>(request.AuditDetailSerialCommands);
            _repository.Update(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}