using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;


namespace WareHouse.API.Application.Queries.DashBoard
{
    public class DashBoardSelectTopItemBeginningCommand : IRequest<IPaginatedList<SelectTopBengingDTO>>
    {
        public string order { get; set; }
    }

    public class DashBoardSelectTopItemBeginningCommandHandler : IRequestHandler<DashBoardSelectTopItemBeginningCommand,
        IPaginatedList<SelectTopBengingDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<SelectTopBengingDTO> _list;

        public DashBoardSelectTopItemBeginningCommandHandler(IDapper repository, IPaginatedList<SelectTopBengingDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<SelectTopBengingDTO>> Handle(DashBoardSelectTopItemBeginningCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            // BuildMyString.com generated code. Please enjoy your string responsibly.

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT top 5  whl.WareHouseId,WareHouse.Name, whi.Code as WareHouseItemCode,   whi.Name as WareHouseItemName,     ");
            sb.Append("  ");
            sb.Append("(SELECT       CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END     ");
            sb.Append("FROM vWareHouseLedger whl     WHERE   whl.ItemId = whi.Id   ) +  ");
            sb.Append("(SELECT       CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END     ");
            sb.Append("FROM Inward i       INNER JOIN InwardDetail Id         ON i.Id = Id.InwardId    ");
            sb.Append("WHERE i.Ondelete=0 and Id.OnDelete=0    AND Id.ItemId = whi.Id)  ");
            sb.Append("- (SELECT       CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END   ");
            sb.Append("FROM Outward o       INNER JOIN OutwardDetail od         ON o.Id = od.OutwardId   ");
            sb.Append("WHERE o.Ondelete=0 and od.OnDelete=0    AND od.ItemId = whi.Id) AS Balance,   ");
            sb.Append("u.UnitName ");
            sb.Append("FROM WareHouseItem whi   INNER JOIN Unit u     ON whi.UnitId = u.Id  ");
            sb.Append("INNER JOIN vWareHouseLedger whl ON whi.Id = whl.ItemId   ");
            sb.Append("inner join WareHouse on whl.WareHouseId=WareHouse.Id ");
            sb.Append("WHERE  whi.OnDelete=0 and u.OnDelete=0  ");
            sb.Append("GROUP BY whi.Id,    whi.Name,    u.UnitName,whi.Code ,whl.WareHouseId,WareHouse.Name ");
            sb.Append("order by Balance ");
            if (request.order == "desc")
                sb.Append("desc ");
            else if (request.order == "asc")
                sb.Append("asc ");
            _list.Result = await _repository.GetList<SelectTopBengingDTO>(sb.ToString(), null, CommandType.Text);
            _list.totalCount = _list.Result.Count();
            return _list;
        }


    }
}