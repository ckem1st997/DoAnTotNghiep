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
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteWareHouseItemCommandHandler : IRequestHandler<DeleteWareHouseItemCommand, bool>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouseItem> _repository;
        private readonly IDapper _dapper;

        public DeleteWareHouseItemCommandHandler(IRepositoryEF<Domain.Entity.WareHouseItem> repository, IDapper dapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _dapper = dapper ?? throw new ArgumentNullException(nameof(dapper));
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