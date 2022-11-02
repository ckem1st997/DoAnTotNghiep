using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteWareHouseItemUnitCommandHandler : IRequestHandler<DeleteWareHouseItemUnitCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemUnit> _repository;
        private readonly IMapper _mapper;

        public DeleteWareHouseItemUnitCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemUnit> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteWareHouseItemUnitCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}