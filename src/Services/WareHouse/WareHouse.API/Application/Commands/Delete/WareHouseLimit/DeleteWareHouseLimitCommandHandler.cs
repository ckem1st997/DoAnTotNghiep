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
    public partial class DeleteWareHouseLimitCommandHandler : IRequestHandler<DeleteWareHouseLimitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseLimit> _repository;
        private readonly IMapper _mapper;

        public DeleteWareHouseLimitCommandHandler(IRepositoryEF<Domain.Entity.WareHouseLimit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteWareHouseLimitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}