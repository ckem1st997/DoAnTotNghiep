using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteWareHouseItemCommandHandler : IRequestHandler<DeleteWareHouseItemCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItem> _repository;

        public DeleteWareHouseItemCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItem> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(DeleteWareHouseItemCommand request, CancellationToken cancellationToken)
        {
            if (request is null)
                return false;
            var res = await _repository.BulkDeleteEditOnDeleteAsync(request.Id);
            return res > 0;
        }
    }
}