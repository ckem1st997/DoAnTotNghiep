using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.Domain.Entity;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetAll.Unit
{

    public class GetWareHouseUnitByIdItemCommand : IRequest<IEnumerable<UnitDTO>>
    {
        public string IdItem { get; set; }
    }
public class GetWareHouseUnitByIdItemCommandHandler : IRequestHandler<GetWareHouseUnitByIdItemCommand, IEnumerable<UnitDTO>>
{
    private readonly IDapper _repository;

    public GetWareHouseUnitByIdItemCommandHandler(IDapper repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<UnitDTO>> Handle(GetWareHouseUnitByIdItemCommand request,
        CancellationToken cancellationToken)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request.IdItem == null) return null;
        const string sql = "select Unit.Id,Unit.UnitName from WareHouseItemUnit inner join Unit on WareHouseItemUnit.UnitId=Unit.Id where ItemId=@itemId and Unit.Inactive=1 and Unit.OnDelete=0 and WareHouseItemUnit.OnDelete=0 ";
        var parameter = new DynamicParameters();
        parameter.Add("@itemId", request.IdItem);
        var getAll = await _repository.GetAllAync<UnitDTO>(sql, parameter, CommandType.Text);
        return getAll;
    }
}
}