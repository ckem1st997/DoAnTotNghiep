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


namespace WareHouse.API.Application.Queries.DashBoard
{
    public class SelectTopDashBoardCommand : IRequest<SelectTopDashBoardDTO>
    {
    }

    public class SelectTopDashBoardCommandHandler : IRequestHandler<SelectTopDashBoardCommand,
        SelectTopDashBoardDTO>
    {
        private readonly IDapper _repository;

        public SelectTopDashBoardCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<SelectTopDashBoardDTO> Handle(SelectTopDashBoardCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            var result = new SelectTopDashBoardDTO();

            var sbbuiderMax = new StringBuilder(" desc ");
            var sbbuiderMin = new StringBuilder(" asc ");
            //
            StringBuilder sbMax = GetTotalItemCount(sbbuiderMax);
            StringBuilder sbMin = GetTotalItemCount(sbbuiderMin); 
            //

            StringBuilder sbbMax = GetWareHouse(sbbuiderMax);
            StringBuilder sbbMin = GetWareHouse(sbbuiderMin);

            result.ItemCountMax = await _repository.GetAyncFirst<DashBoardSelectTopInAndOut>(sbMax.ToString(), null, CommandType.Text);
            result.ItemCountMin = await _repository.GetAyncFirst<DashBoardSelectTopInAndOut>(sbMin.ToString(), null, CommandType.Text);
            result.WareHouseBeginningCountMax = await _repository.GetAyncFirst<SelectTopWareHouseDTO>(sbbMax.ToString(), null, CommandType.Text);
            result.WareHouseBeginningCountMin = await _repository.GetAyncFirst<SelectTopWareHouseDTO>(sbbMin.ToString(), null, CommandType.Text);
            return result;
        }

        private static StringBuilder GetTotalItemCount(StringBuilder builder)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select top 1 d1.Name,d1.Code,d1.Id,d1.UnitName,sum(d1.Count) as Count,sum(d1.SumPrice) as SumPrice,sum(d1.SumQuantity) as SumQuantity from ");
            sb.Append("(select count(InwardDetail.ItemId) as Count,WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,sum(InwardDetail.Quantity) as SumQuantity,Unit.UnitName,SUM(Price)as SumPrice ");
            sb.Append("from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId  ");
            sb.Append("inner join WareHouseItem on InwardDetail.ItemId=WareHouseItem.Id ");
            sb.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sb.Append("where Inward.OnDelete=0 and InwardDetail.OnDelete=0 and WareHouseItem.OnDelete=0 and Unit.OnDelete=0 ");
            sb.Append("group by WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,Unit.UnitName ");
            sb.Append("union all ");
            sb.Append("select count(OutwardDetail.ItemId) as Count,WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,sum(OutwardDetail.Quantity) as SumQuantity,Unit.UnitName, SUM(Price) as SumPrice ");
            sb.Append("from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId  ");
            sb.Append("inner join WareHouseItem on OutwardDetail.ItemId=WareHouseItem.Id ");
            sb.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sb.Append("where Outward.OnDelete=0 and OutwardDetail.OnDelete=0 and WareHouseItem.OnDelete=0 and Unit.OnDelete=0 ");
            sb.Append("group by WareHouseItem.Name,WareHouseItem.Code,WareHouseItem.Id,Unit.UnitName) d1 ");
            sb.Append("group by d1.Name,d1.Code,d1.Id,d1.UnitName ");
            sb.Append("order by sum(d1.Count)   ");
            sb.Append(builder);
            return sb;
        }

        private static StringBuilder GetWareHouse(StringBuilder builder)
        {
            StringBuilder sbb = new StringBuilder();



            sbb.Append("select top 1 d1.WareHouseId,d1.Name,sum(d1.Balance) as SumBalance from ");
            sbb.Append("(SELECT whl.WareHouseId,WareHouse.Name,     ");
            sbb.Append("  ");
            sbb.Append("(SELECT       CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END     ");
            sbb.Append("FROM vWareHouseLedger whl     WHERE   whl.ItemId = whi.Id   ) +  ");
            sbb.Append("(SELECT       CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END     ");
            sbb.Append("FROM Inward i       INNER JOIN InwardDetail Id         ON i.Id = Id.InwardId    ");
            sbb.Append("WHERE i.Ondelete=0 and Id.OnDelete=0    AND Id.ItemId = whi.Id)  ");
            sbb.Append("- (SELECT       CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END   ");
            sbb.Append("FROM Outward o       INNER JOIN OutwardDetail od         ON o.Id = od.OutwardId   ");
            sbb.Append("WHERE o.Ondelete=0 and od.OnDelete=0    AND od.ItemId = whi.Id) AS Balance ");
            sbb.Append("FROM WareHouseItem whi   INNER JOIN Unit u     ON whi.UnitId = u.Id  ");
            sbb.Append("INNER JOIN vWareHouseLedger whl ON whi.Id = whl.ItemId   ");
            sbb.Append("inner join WareHouse on whl.WareHouseId=WareHouse.Id ");
            sbb.Append("WHERE  whi.OnDelete=0 and u.OnDelete=0  ");
            sbb.Append("GROUP BY whi.Id,whl.WareHouseId,WareHouse.Name)d1 ");
            sbb.Append("GROUP BY d1.WareHouseId,d1.Name ");
            sbb.Append("order by sum(d1.Balance)   ");
            sbb.Append(builder);
            return sbb;
        }
    }
}