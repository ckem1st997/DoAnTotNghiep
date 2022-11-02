using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Share.BaseCore.IRepositories;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateAuditDetailSerialCommandHandler: IRequestHandler<CreateAuditDetailSerialCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.AuditDetailSerial> _repository;
        private readonly IMapper _mapper;

        public CreateAuditDetailSerialCommandHandler(IRepositoryEF<Domain.Entity.AuditDetailSerial> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateAuditDetailSerialCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var result = _mapper.Map<Domain.Entity.AuditDetailSerial>(request.AuditDetailSerialCommands);
            await _repository.AddAsync(result);
            return await _repository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0;

        }
    }
}