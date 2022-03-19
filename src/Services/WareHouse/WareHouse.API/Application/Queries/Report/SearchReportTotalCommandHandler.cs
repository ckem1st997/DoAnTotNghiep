using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;


namespace WareHouse.API.Application.Queries.Report
{
    public class SearchReportTotalCommand : BaseSearchModel, IRequest<IPaginatedList<ReportValueModel>>
    {
        public string WareHouseItemId { get; set; }
        public string WareHouseId { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Proposer { get; set; }

        public string DepartmentId { get; set; }

        public string ProjectId { get; set; }
        public bool Excel { get; set; }
    }

    public class SearchReportTotalCommandHandler : IRequestHandler<SearchReportTotalCommand,
        IPaginatedList<ReportValueModel>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<ReportValueModel> _list;

        public SearchReportTotalCommandHandler(IDapper repository, IPaginatedList<ReportValueModel> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<ReportValueModel>> Handle(SearchReportTotalCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");
            sb.Append("  whi.Code as WareHouseItemCode, ");
            sb.Append("  whi.Name as WareHouseItemName, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END ");
            sb.Append("    FROM vWareHouseLedger whl ");
            sb.Append("    WHERE whl.WareHouseId = @pWareHouseId ");
            sb.Append("    AND whl.ItemId = whi.Id ");
            sb.Append("    AND whl.VoucherDate < @pFrom) AS Beginning, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END ");
            sb.Append("    FROM Inward i ");
            sb.Append("      INNER JOIN InwardDetail Id ");
            sb.Append("        ON i.Id = Id.InwardId ");
            sb.Append("    WHERE i.VoucherDate BETWEEN @pFrom AND @pTo ");
            sb.Append("    AND i.WareHouseId = @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND Id.ItemId = @pWareHouseItemId ");
            sb.Append("    AND Id.ItemId = whi.Id) AS Import, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END ");
            sb.Append("    FROM Outward o ");
            sb.Append("      INNER JOIN OutwardDetail od ");
            sb.Append("        ON o.Id = od.OutwardId ");
            sb.Append("    WHERE o.VoucherDate BETWEEN @pFrom AND @pTo ");
            sb.Append("    AND o.WareHouseId = @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND od.ItemId = @pWareHouseItemId ");
            sb.Append("    AND od.ItemId = whi.Id) AS Export, ");
            sb.Append("  (SELECT ");
            sb.Append("      CASE WHEN SUM(whl.Quantity) IS NULL THEN 0 ELSE SUM(whl.Quantity) END ");
            sb.Append("    FROM vWareHouseLedger whl ");
            sb.Append("    WHERE whl.WareHouseId = @pWareHouseId ");
            sb.Append("    AND whl.ItemId = whi.Id ");
            sb.Append("    AND whl.VoucherDate < @pFrom) + (SELECT ");
            sb.Append("      CASE WHEN SUM(Id.Quantity) IS NULL THEN 0 ELSE SUM(Id.Quantity) END ");
            sb.Append("    FROM Inward i ");
            sb.Append("      INNER JOIN InwardDetail Id ");
            sb.Append("        ON i.Id = Id.InwardId ");
            sb.Append("    WHERE i.VoucherDate BETWEEN @pFrom AND @pTo ");
            sb.Append("    AND i.WareHouseId = @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND Id.ItemId = @pWareHouseItemId ");
            sb.Append("    AND Id.ItemId = whi.Id) - (SELECT ");
            sb.Append("      CASE WHEN SUM(od.Quantity) IS NULL THEN 0 ELSE SUM(od.Quantity) END ");
            sb.Append("    FROM Outward o ");
            sb.Append("      INNER JOIN OutwardDetail od ");
            sb.Append("        ON o.Id = od.OutwardId ");
            sb.Append("    WHERE o.VoucherDate BETWEEN @pFrom  AND @pTo ");
            sb.Append("    AND o.WareHouseId = @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append("    AND od.ItemId = @pWareHouseItemId ");
            sb.Append("    AND od.ItemId = whi.Id) AS Balance, ");
            sb.Append("  u.UnitName ");
            sb.Append("FROM WareHouseItem whi ");
            sb.Append("  INNER JOIN Unit u ");
            sb.Append("    ON whi.UnitId = u.Id INNER JOIN vWareHouseLedger whl ON whi.Id = whl.ItemId  ");
            sb.Append("  WHERE whl.WareHouseId= @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sb.Append(" and whi.Id = @pWareHouseItemId ");
            sb.Append("GROUP BY whi.Id, ");
            sb.Append("         whi.Name, ");
            sb.Append("         u.UnitName,whi.Code ");
            sb.Append("ORDER BY whi.Name ");
            if (!request.Excel)
                sb.Append(" LIMIT @p2 OFFSET @p3 ");

            //count


            StringBuilder sbCount = new StringBuilder();

            sbCount.Append("SELECT COUNT(*) FROM ( ");
            sbCount.Append("SELECT ");
            sbCount.Append("  whi.Code as WareHouseItemCode ");
            sbCount.Append("FROM WareHouseItem whi ");
            sbCount.Append("  INNER JOIN Unit u ");
            sbCount.Append("    ON whi.UnitId = u.Id INNER JOIN vWareHouseLedger whl ON whi.Id = whl.ItemId  ");
            sbCount.Append("  WHERE whl.WareHouseId= @pWareHouseId ");
            if (!string.IsNullOrEmpty(request.WareHouseItemId))
                sbCount.Append("and whi.Id = @pWareHouseItemId ");
            sbCount.Append("GROUP BY whi.Id, ");
            sbCount.Append("         whi.Name ");
            sbCount.Append("              ) t   ");
            sbCount.Append("  ");

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p2", request.Skip);
            parameter.Add("@p3", request.Take);
            parameter.Add("@pWareHouseId", request.WareHouseId);
            parameter.Add("@pWareHouseItemId", request.WareHouseItemId);
             parameter.Add("@pFrom", request.FromDate);
            parameter.Add("@pTo", request.ToDate);
         
            // parameter.Add("@pFrom", "" + request.FromDate.Value.Year + "-" + request.FromDate.Value.Month + "-" + request.FromDate.Value.Day + "  12:0:0 AM ");
            // parameter.Add("@pTo", "" + request.ToDate.Value.Year + "-" + request.ToDate.Value.Month + "-" + request.ToDate.Value.Day + "  12:0:0 AM  ");
            // Console.WriteLine(sb.ToString());
            _list.Result = await _repository.GetList<ReportValueModel>(sb.ToString(), parameter);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter);
            return _list;
        }
    }
}