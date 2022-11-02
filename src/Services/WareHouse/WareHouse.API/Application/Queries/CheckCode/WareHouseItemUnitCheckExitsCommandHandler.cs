using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class WareHouseItemUnitCheckExitsCommand : IRequest<bool>
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
    }
    public class WareHouseItemUnitCheckExitsCommandHandler : IRequestHandler<WareHouseItemUnitCheckExitsCommand, bool>
    {
        private readonly IDapper _repository;

        public WareHouseItemUnitCheckExitsCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<bool> Handle(WareHouseItemUnitCheckExitsCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var sql = "select Id from WareHouseItemUnit where ItemId=@itemId and UnitId=@unitId";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@itemId", request.ItemId);
            parameter.Add("@unitId", request.UnitId);
            var res = await _repository.GetAyncFirst<BaseModel>(sql, parameter, CommandType.Text);
            return res != null;
        }
    }
}