using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class WareHouseItemCodeCommand : IRequest<bool>
    {
        public string Code { get; set; }
    }
    public class WareHouseItemCodeCommandHandler : IRequestHandler<WareHouseItemCodeCommand, bool>
    {
        private readonly IDapper _repository;
        public WareHouseItemCodeCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<bool> Handle(WareHouseItemCodeCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var res = await _repository.CheckCode<Domain.Entity.WareHouse>(request.Code, nameof(Domain.Entity.WareHouseItem));
            return res > 0;
        }
    }
}