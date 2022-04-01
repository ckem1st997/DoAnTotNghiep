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
    public class DashBoardChartInAndOutCountByDayCommand : IRequest<DashBoardChartInAndOutCount>
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
    }

    public class DashBoardChartInAndOutCountByDayCommandHandler : IRequestHandler<DashBoardChartInAndOutCountByDayCommand, DashBoardChartInAndOutCount>
    {
        private readonly IDapper _repository;

        public DashBoardChartInAndOutCountByDayCommandHandler(IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<DashBoardChartInAndOutCount> Handle(DashBoardChartInAndOutCountByDayCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            var result = new DashBoardChartInAndOutCount();
            StringBuilder sb = new StringBuilder();

            sb.Append(";WITH d(d) AS  ");
            sb.Append("( ");
            sb.Append("  SELECT DATEADD(DAY, n, DATEADD(DAY, DATEDIFF(DAY, 0,  @fromDate), 0)) ");
            sb.Append("  FROM ( SELECT TOP (DATEDIFF(DAY,   @fromDate,  @toDate) + 1)  ");
            sb.Append("    n = ROW_NUMBER() OVER (ORDER BY [object_id]) - 1 ");
            sb.Append("    FROM sys.all_objects ORDER BY [object_id] ) AS n ");
            sb.Append(") ");
            sb.Append("SELECT  ");
            sb.Append("  [Name]    = DATENAME(DAY, d.d),  ");
            sb.Append("  [NameParent]     = Month(d.d),  ");
            sb.Append("  Count = COUNT(o.Id)  ");
            sb.Append("FROM d LEFT OUTER JOIN Inward AS o ");
            sb.Append("  ON o.VoucherDate >= d.d ");
            sb.Append("  AND o.VoucherDate <= DATEADD(DAY, 1, d.d) ");
            sb.Append("GROUP BY d.d ");
            sb.Append("ORDER BY d.d; ");


            StringBuilder sbOut = new StringBuilder();

            sbOut.Append(";WITH d(d) AS  ");
            sbOut.Append("( ");
            sbOut.Append("  SELECT DATEADD(DAY, n, DATEADD(DAY, DATEDIFF(DAY, 0,  @fromDate), 0)) ");
            sbOut.Append("  FROM ( SELECT TOP (DATEDIFF(DAY,   @fromDate,  @toDate) + 1)  ");
            sbOut.Append("    n = ROW_NUMBER() OVER (ORDER BY [object_id]) - 1 ");
            sbOut.Append("    FROM sys.all_objects ORDER BY [object_id] ) AS n ");
            sbOut.Append(") ");
            sbOut.Append("SELECT  ");
            sbOut.Append("  [Name]    = DATENAME(DAY, d.d),  ");
            sbOut.Append("  [NameParent]     = Month(d.d),  ");
            sbOut.Append("  Count = COUNT(o.Id)  ");
            sbOut.Append("FROM d LEFT OUTER JOIN Outward AS o ");
            sbOut.Append("  ON o.VoucherDate >= d.d ");
            sbOut.Append("  AND o.VoucherDate <= DATEADD(DAY, 1, d.d) ");
            sbOut.Append("GROUP BY d.d ");
            sbOut.Append("ORDER BY d.d; ");

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@fromDate", ExtensionFull.GetDateToSqlRaw(request.fromDate));
            parameter.Add("@toDate", ExtensionFull.GetDateToSqlRaw(request.toDate));
            result.Inward = await _repository.GetList<BaseCountChartByMouthOrYear>(sb.ToString(), parameter, CommandType.Text);
            result.Outward = await _repository.GetList<BaseCountChartByMouthOrYear>(sbOut.ToString(), parameter, CommandType.Text);
            return result;
        }


    }
}