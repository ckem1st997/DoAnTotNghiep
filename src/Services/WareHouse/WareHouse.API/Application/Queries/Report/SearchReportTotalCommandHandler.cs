using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;


namespace WareHouse.API.Application.Queries.Report
{
    public class SearchReportTotalCommand : BaseSearchModel, IRequest<IPaginatedList<ReportValueTotalDT0>>
    {
        public string WareHouseItemId { get; set; }
        public string WareHouseId { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string ProjectId { get; set; }
        public bool Excel { get; set; }
    }

    public class SearchReportTotalCommandHandler : IRequestHandler<SearchReportTotalCommand,
        IPaginatedList<ReportValueTotalDT0>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<ReportValueTotalDT0> _list;

        public SearchReportTotalCommandHandler(IDapper repository, IPaginatedList<ReportValueTotalDT0> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<ReportValueTotalDT0>> Handle(SearchReportTotalCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;

            //get list id Chidren
            var departmentIds = new List<string>();
            if (!string.IsNullOrEmpty(request.WareHouseId))
            {
                StringBuilder GetListChidren = new StringBuilder();
                GetListChidren.Append("with cte (Id, Name, ParentId) as ( ");
                GetListChidren.Append("  select     wh.Id, ");
                GetListChidren.Append("             wh.Name, ");
                GetListChidren.Append("             wh.ParentId ");
                GetListChidren.Append("  from       WareHouse wh ");
                GetListChidren.Append("  where      wh.ParentId=@WareHouseId and  wh.OnDelete=0 ");
                GetListChidren.Append("  union all ");
                GetListChidren.Append("  SELECT     p.Id, ");
                GetListChidren.Append("             p.Name, ");
                GetListChidren.Append("             p.ParentId ");
                GetListChidren.Append("  from       WareHouse  p  ");
                GetListChidren.Append("  inner join cte ");
                GetListChidren.Append("          on p.ParentId = cte.id where p.OnDelete=0 ");
                GetListChidren.Append(") ");
                GetListChidren.Append(" select cte.Id FROM cte GROUP BY cte.Id,cte.Name,cte.ParentId; ");
                DynamicParameters parameterwh = new DynamicParameters();
                parameterwh.Add("@WareHouseId", request.WareHouseId);
                departmentIds =
                    (List<string>)await _repository.GetList<string>(GetListChidren.ToString(), parameterwh,
                        CommandType.Text);
                departmentIds.Add(request.WareHouseId);
            }




            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");
            sb.Append("  whi.Code as WareHouseItemCode, ");
            sb.Append("  whi.Name as WareHouseItemName, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END ");
            sb.Append("    FROM vWareHouseLedger whl ");
            sb.Append("    WHERE whl.WareHouseId   in @pWareHouseId ");
            sb.Append("    AND whl.ItemId = whi.Id ");
            sb.Append("    AND whl.VoucherDate < @pFrom) AS Beginning, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END ");
            sb.Append("    FROM Inward i ");
            sb.Append("      INNER JOIN InwardDetail Id ");
            sb.Append("        ON i.Id = Id.InwardId ");
            sb.Append("    WHERE i.VoucherDate BETWEEN @pFrom AND @pTo and i.Ondelete=0 and Id.OnDelete=0 ");
            sb.Append("    AND i.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND Id.ItemId = @pWareHouseItemId ");
            sb.Append("    AND Id.ItemId = whi.Id) AS Import, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END ");
            sb.Append("    FROM Outward o ");
            sb.Append("      INNER JOIN OutwardDetail od ");
            sb.Append("        ON o.Id = od.OutwardId ");
            sb.Append("    WHERE o.VoucherDate BETWEEN @pFrom AND @pTo  and o.Ondelete=0 and od.OnDelete=0  ");
            sb.Append("    AND o.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND od.ItemId = @pWareHouseItemId ");
            sb.Append("    AND od.ItemId = whi.Id) AS Export, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END ");
            sb.Append("    FROM vWareHouseLedger whl ");
            sb.Append("    WHERE whl.WareHouseId   in @pWareHouseId ");
            sb.Append("    AND whl.ItemId = whi.Id ");
            sb.Append("    AND whl.VoucherDate < @pFrom) + (SELECT ");
            sb.Append("      CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END ");
            sb.Append("    FROM Inward i ");
            sb.Append("      INNER JOIN InwardDetail Id ");
            sb.Append("        ON i.Id = Id.InwardId ");
            sb.Append("    WHERE i.VoucherDate BETWEEN @pFrom AND @pTo  and i.Ondelete=0 and Id.OnDelete=0 ");
            sb.Append("    AND i.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND Id.ItemId = @pWareHouseItemId ");
            sb.Append("    AND Id.ItemId = whi.Id) - (SELECT ");
            sb.Append("      CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END ");
            sb.Append("    FROM Outward o ");
            sb.Append("      INNER JOIN OutwardDetail od ");
            sb.Append("        ON o.Id = od.OutwardId ");
            sb.Append("    WHERE o.VoucherDate BETWEEN @pFrom  AND @pTo and o.Ondelete=0 and od.OnDelete=0 ");
            sb.Append("    AND o.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND od.ItemId = @pWareHouseItemId ");
            sb.Append("    AND od.ItemId = whi.Id) AS Balance, ");
            sb.Append("  u.UnitName ");
            sb.Append("FROM WareHouseItem whi ");
            sb.Append("  INNER JOIN Unit u ");
            sb.Append("    ON whi.UnitId = u.Id INNER JOIN vWareHouseLedger whl ON whi.Id = whl.ItemId  ");
            sb.Append("  WHERE whl.WareHouseId  in @pWareHouseId  and whi.OnDelete=0 and u.OnDelete=0 ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append(" and whi.Id = @pWareHouseItemId ");
            sb.Append("GROUP BY whi.Id, ");
            sb.Append("         whi.Name, ");
            sb.Append("         u.UnitName,whi.Code ");
            sb.Append("ORDER BY whi.Name ");
            if (!request.Excel)
                sb.Append(" OFFSET @p2 ROWS FETCH NEXT @p3 ROWS ONLY");

            //count


            StringBuilder sbCount = new StringBuilder();


            sbCount.Append("SELECT COUNT(*) FROM ( ");
                       sbCount.Append("SELECT ");
            sbCount.Append("  whi.Code as WareHouseItemCode, ");
            sbCount.Append("  whi.Name as WareHouseItemName, ");
            sbCount.Append("  (SELECT ");
            sbCount.Append("      CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END ");
            sbCount.Append("    FROM vWareHouseLedger whl ");
            sbCount.Append("    WHERE whl.WareHouseId   in @pWareHouseId ");
            sbCount.Append("    AND whl.ItemId = whi.Id ");
            sbCount.Append("    AND whl.VoucherDate < @pFrom) AS Beginning, ");
            sbCount.Append("  (SELECT ");
            sbCount.Append("      CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END ");
            sbCount.Append("    FROM Inward i ");
            sbCount.Append("      INNER JOIN InwardDetail Id ");
            sbCount.Append("        ON i.Id = Id.InwardId ");
            sbCount.Append("    WHERE i.VoucherDate BETWEEN @pFrom AND @pTo and i.Ondelete=0 and Id.OnDelete=0 ");
            sbCount.Append("    AND i.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sbCount.Append("    AND Id.ItemId = @pWareHouseItemId ");
            sbCount.Append("    AND Id.ItemId = whi.Id) AS Import, ");
            sbCount.Append("  (SELECT ");
            sbCount.Append("      CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END ");
            sbCount.Append("    FROM Outward o ");
            sbCount.Append("      INNER JOIN OutwardDetail od ");
            sbCount.Append("        ON o.Id = od.OutwardId ");
            sbCount.Append("    WHERE o.VoucherDate BETWEEN @pFrom AND @pTo  and o.Ondelete=0 and od.OnDelete=0  ");
            sbCount.Append("    AND o.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sbCount.Append("    AND od.ItemId = @pWareHouseItemId ");
            sbCount.Append("    AND od.ItemId = whi.Id) AS Export, ");
            sbCount.Append("  (SELECT ");
            sbCount.Append("      CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END ");
            sbCount.Append("    FROM vWareHouseLedger whl ");
            sbCount.Append("    WHERE whl.WareHouseId   in @pWareHouseId ");
            sbCount.Append("    AND whl.ItemId = whi.Id ");
            sbCount.Append("    AND whl.VoucherDate < @pFrom) + (SELECT ");
            sbCount.Append("      CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END ");
            sbCount.Append("    FROM Inward i ");
            sbCount.Append("      INNER JOIN InwardDetail Id ");
            sbCount.Append("        ON i.Id = Id.InwardId ");
            sbCount.Append("    WHERE i.VoucherDate BETWEEN @pFrom AND @pTo  and i.Ondelete=0 and Id.OnDelete=0 ");
            sbCount.Append("    AND i.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sbCount.Append("    AND Id.ItemId = @pWareHouseItemId ");
            sbCount.Append("    AND Id.ItemId = whi.Id) - (SELECT ");
            sbCount.Append("      CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END ");
            sbCount.Append("    FROM Outward o ");
            sbCount.Append("      INNER JOIN OutwardDetail od ");
            sbCount.Append("        ON o.Id = od.OutwardId ");
            sbCount.Append("    WHERE o.VoucherDate BETWEEN @pFrom  AND @pTo and o.Ondelete=0 and od.OnDelete=0 ");
            sbCount.Append("    AND o.WareHouseId   in @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sbCount.Append("    AND od.ItemId = @pWareHouseItemId ");
            sbCount.Append("    AND od.ItemId = whi.Id) AS Balance, ");
            sbCount.Append("  u.UnitName ");
            sbCount.Append("FROM WareHouseItem whi ");
            sbCount.Append("  INNER JOIN Unit u ");
            sbCount.Append("    ON whi.UnitId = u.Id INNER JOIN vWareHouseLedger whl ON whi.Id = whl.ItemId  ");
            sbCount.Append("  WHERE whl.WareHouseId  in @pWareHouseId  and whi.OnDelete=0 and u.OnDelete=0 ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sbCount.Append(" and whi.Id = @pWareHouseItemId ");
            sbCount.Append("GROUP BY whi.Id, ");
            sbCount.Append("         whi.Name, ");
            sbCount.Append("         u.UnitName,whi.Code ");
            sbCount.Append("              ) t   ");
            sbCount.Append("  ");

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p2", request.Skip);
            parameter.Add("@p3", request.Take);
            parameter.Add("@pWareHouseId", departmentIds);
            parameter.Add("@pWareHouseItemId", request.WareHouseItemId);
            parameter.Add("@pFrom", ExtensionFull.GetDateToSqlRaw(request.FromDate));
            parameter.Add("@pTo", ExtensionFull.GetDateToSqlRaw(request.ToDate));
            _list.Result = await _repository.GetList<ReportValueTotalDT0>(sb.ToString(), parameter, commandType: CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, commandType: CommandType.Text);
            return _list;
        }


    }
}