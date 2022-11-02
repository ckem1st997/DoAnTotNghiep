using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteAuditCommandHandler : IRequestHandler<DeleteAuditCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.Audit> _repository;
        private readonly IMapper _mapper;

        public DeleteAuditCommandHandler(IRepositoryEF<Domain.Entity.Audit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteAuditCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}