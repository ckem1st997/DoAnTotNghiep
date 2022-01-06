using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteAuditCouncilCommandHandler : IRequestHandler<DeleteAuditCouncilCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.AuditCouncil> _repository;
        private readonly IMapper _mapper;

        public DeleteAuditCouncilCommandHandler(IRepositoryEF<Domain.Entity.AuditCouncil> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteAuditCouncilCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}