using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteInwardDetailCommandHandler : IRequestHandler<DeleteInwardDetailCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.InwardDetail> _repository;
        private readonly IMapper _mapper;

        public DeleteInwardDetailCommandHandler(IRepositoryEF<Domain.Entity.InwardDetail> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteInwardDetailCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}