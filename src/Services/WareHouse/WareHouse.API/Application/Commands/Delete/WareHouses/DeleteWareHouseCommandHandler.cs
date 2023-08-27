using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteWareHouseCommandHandler : IRequestHandler<DeleteWareHouseCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IMapper _mapper;

        public DeleteWareHouseCommandHandler(IRepositoryEF<Domain.Entity.WareHouse> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteWareHouseCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
           await _repository.DeteleSoftDelete(request.Id, cancellationToken);
            var res = await _repository.SaveChangesConfigureAwaitAsync(cancellationToken: cancellationToken);
            return res > 0;
        }
    }
}