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
    public partial class DeleteWareHouseItemCategoryCommandHandler : IRequestHandler<DeleteWareHouseItemCategoryCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItemCategory> _repository;
        private readonly IMapper _mapper;

        public DeleteWareHouseItemCategoryCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItemCategory> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteWareHouseItemCategoryCommand request, CancellationToken cancellationToken)
        {

            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}