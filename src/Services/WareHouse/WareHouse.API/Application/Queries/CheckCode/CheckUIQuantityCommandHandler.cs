using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouses;

namespace WareHouse.API.Application.Querie.CheckCode
{
    public class CheckUIQuantityCommand : IRequest<decimal>
    {
        public string WareHouseId { get; set; }
        public string ItemId { get; set; }
    }
    public class CheckUIQuantityCommandHandler : IRequestHandler<CheckUIQuantityCommand, decimal>
    {
        private readonly IDapper _repository;

        public CheckUIQuantityCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<decimal> Handle(CheckUIQuantityCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null || string.IsNullOrEmpty(request.ItemId) || string.IsNullOrEmpty(request.WareHouseId))
                throw new ArgumentNullException(nameof(request));
            var date = DateTime.UtcNow;
            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT ");
            sb.Append("  SUM(d1.Quantity) AS Amount ");
            sb.Append("FROM (SELECT ");
            sb.Append("    bwh.ItemId, ");
            sb.Append("    bwh.WarehouseId, ");
            sb.Append("    bwh.Quantity ");
            sb.Append("  FROM BeginningWareHouse bwh ");
            sb.Append("  WHERE bwh.ItemId = @p1 and bwh.OnDelete=0 ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT ");
            sb.Append("    id.ItemId, ");
            sb.Append("    i.WarehouseId, ");
            sb.Append("    id.Quantity ");
            sb.Append("  FROM Inward i ");
            sb.Append("    INNER JOIN InwardDetail id ");
            sb.Append("      ON i.id = id.InwardId ");
            sb.Append("  WHERE id.ItemId = @p1 and id.OnDelete=0 and i.OnDelete=0 ");
          //  sb.Append("  AND i.VoucherDate <= '" + ConvertDateTimeToDateTimeSql(date) + " 12:0:0 AM' ");
            sb.Append("  UNION ALL ");
            sb.Append("  SELECT ");
            sb.Append("    od.ItemId, ");
            sb.Append("    o.WarehouseId, ");
            sb.Append("    -od.Quantity ");
            sb.Append("  FROM Outward o ");
            sb.Append("    INNER JOIN OutwardDetail od ");
            sb.Append("      ON o.Id = od.OutwardId ");
            sb.Append("  WHERE od.ItemId =@p1  and od.OnDelete=0 and o.OnDelete=0  ");
            //  sb.Append("  AND o.VoucherDate <= '" + ConvertDateTimeToDateTimeSql(date) + " 12:0:0 AM'");
            sb.Append(" ) d1  ");
            sb.Append("  INNER JOIN WareHouse wh ");
            sb.Append("    ON d1.WarehouseId = wh.Id ");
            sb.Append("    WHERE d1.WareHouseId=@p2  and wh.OnDelete=0 ");
            sb.Append("GROUP BY d1.ItemId, ");
            sb.Append("         d1.WarehouseId  ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@p1", request.ItemId);
            parameter.Add("@p2", request.WareHouseId);
            var res = await _repository.GetAyncFirst<decimal>(sb.ToString(),parameter, CommandType.Text);
            return res;
        }

        private static string ConvertDateTimeToDateTimeSql(DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return "";
            return "" + dateTime.Value.Year + "-" + dateTime.Value.Month + "-" + dateTime.Value.Day + "";
        }
    }
}