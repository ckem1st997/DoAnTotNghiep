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
    public class SearchReportDetailsCommand : BaseSearchModel, IRequest<IPaginatedList<ReportValueDetalsDT0>>
    {
        public string WareHouseItemId { get; set; }
        public string WareHouseId { get; set; }
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public bool Excel { get; set; }
    }

    public class SearchReportDetailsCommandHandler : IRequestHandler<SearchReportDetailsCommand,
        IPaginatedList<ReportValueDetalsDT0>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<ReportValueDetalsDT0> _list;

        public SearchReportDetailsCommandHandler(IDapper repository, IPaginatedList<ReportValueDetalsDT0> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<ReportValueDetalsDT0>> Handle(SearchReportDetailsCommand request,
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




            // BuildMyString.com generated code. Please enjoy your string responsibly.

            StringBuilder sb = new StringBuilder();

            sb.Append("select d1.VoucherDate,WareHouseItem.Code,WareHouseItem.Name,d1.VoucherCode,Unit.UnitName, ");
            sb.Append("   COALESCE( ");
            sb.Append("   COALESCE((SELECT COALESCE( bwh.Quantity,0) FROM BeginningWareHouse bwh where bwh.OnDelete = 0 and bwh.ItemId = @pWareHouseItemId  and bwh.WareHouseId  in @pWareHouseId and bwh.CreatedDate <= d1.VoucherDate),0) ");
            sb.Append("   +  ");
            sb.Append("   (SELECT ");
            sb.Append("   COALESCE(sum(id.Quantity),0) ");
            sb.Append("FROM (Inward i ");
            sb.Append("  JOIN InwardDetail id ");
            sb.Append("    ON ((i.Id = id.InwardId and i.OnDelete=0 and id.OnDelete=0 and id.ItemId=@pWareHouseItemId and i.WareHouseID  in @pWareHouseId   and i.VoucherDate <d1.VoucherDate))) ");
            sb.Append(")-(SELECT ");
            sb.Append("  COALESCE(sum(od.Quantity),0) ");
            sb.Append("FROM (Outward o ");
            sb.Append("  JOIN OutwardDetail od ");
            sb.Append("    ON ((o.Id = od.OutwardId and o.OnDelete=0 and od.OnDelete=0 and od.ItemId=@pWareHouseItemId and o.WareHouseID  in @pWareHouseId  and o.VoucherDate<d1.VoucherDate))) ");
            sb.Append("),0) as Beginning, ");
            sb.Append("  (SELECT ");
            sb.Append("  COALESCE(sum(id.Quantity),0) ");
            sb.Append("FROM (Inward i ");
            sb.Append("  JOIN InwardDetail id ");
            sb.Append("    ON ((i.Id = id.InwardId and i.OnDelete=0 and id.OnDelete=0 and id.ItemId=@pWareHouseItemId and i.WareHouseID  in @pWareHouseId   and i.VoucherDate=d1.VoucherDate))) ");
            sb.Append(") as Import, ");
            sb.Append("(SELECT ");
            sb.Append("  COALESCE(sum(od.Quantity),0) ");
            sb.Append("FROM (Outward o ");
            sb.Append("  JOIN OutwardDetail od ");
            sb.Append("    ON ((o.Id = od.OutwardId and o.OnDelete=0 and od.OnDelete=0 and od.ItemId=@pWareHouseItemId and o.WareHouseID  in @pWareHouseId   and o.VoucherDate=d1.VoucherDate ))) ");
            sb.Append(") as Export,d1.Reason,d1.EmployeeName,d1.DepartmentName,d1.ProjectName,d1.Description from (SELECT ");
            sb.Append("  bwh.ItemId AS ItemId, ");
            sb.Append("  COALESCE(bwh.Quantity,0) as Quantity, ");
            sb.Append("  bwh.CreatedDate as VoucherDate, ");
            sb.Append("   0 as Beginning, ");
            sb.Append(" 0 as Import, ");
            sb.Append("0 as Export, ");
            sb.Append("bwh.WareHouseId, ");
            sb.Append("'CDK' as VoucherCode, ");
            sb.Append("'' as Reason, ");
            sb.Append("'' as EmployeeName,'' as DepartmentName,'' as ProjectName,'' as Description ");
            sb.Append("FROM BeginningWareHouse bwh ");
            sb.Append("where bwh.OnDelete=0 and bwh.ItemId=@pWareHouseItemId  and bwh.WareHouseId  in @pWareHouseId   ");
            sb.Append("UNION all ");
            sb.Append("SELECT DISTINCT InwardDetail.ItemId as ItemId,0 as Quantity,Inward.VoucherDate as VoucherDate, 0 as Beginning,0 as Import,0 as Export,Inward.WareHouseID,Inward.VoucherCode as VoucherCode,Inward.Reason,InwardDetail.EmployeeName,InwardDetail.DepartmentName,InwardDetail.ProjectName,Inward.Description from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId where Inward.OnDelete=0 and InwardDetail.OnDelete=0 and InwardDetail.ItemId=@pWareHouseItemId and Inward.WareHouseID  in @pWareHouseId   ");
            sb.Append("UNION all ");
            sb.Append("SELECT DISTINCT OutwardDetail.ItemId as ItemId,0 as Quantity,Outward.VoucherDate as VoucherDate, 0 as Beginning,0 as Import,0 as Export,Outward.WareHouseID as WareHouseId, Outward.VoucherCode,Outward.Reason,OutwardDetail.EmployeeName,OutwardDetail.DepartmentName,OutwardDetail.ProjectName,Outward.Description from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId where Outward.OnDelete=0 and OutwardDetail.OnDelete=0 and OutwardDetail.ItemId=@pWareHouseItemId and Outward.WareHouseID  in @pWareHouseId  ) as d1 ");
            sb.Append("inner join WareHouseItem on WareHouseItem.Id=d1.ItemId ");
            sb.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sb.Append("where d1.VoucherDate BETWEEN @pFrom  AND @pTo ");
            if (!string.IsNullOrEmpty(request.KeySearch))
                sb.Append("and (d1.VoucherCode like @key or d1.DepartmentName like @key or d1.EmployeeName like @key or d1.Description like @key or d1.ProjectName like @key) ");
            sb.Append("group by d1.VoucherDate,d1.Quantity,d1.Beginning,d1.Import,d1.Export,WareHouseItem.Code,WareHouseItem.Name,d1.VoucherCode,Unit.UnitName,d1.Reason,d1.EmployeeName,d1.DepartmentName,d1.ProjectName,d1.Description ");
            sb.Append(" order by d1.VoucherDate");
            if (!request.Excel)
                sb.Append(" OFFSET @p2 ROWS FETCH NEXT @p3 ROWS ONLY");

            //count


            StringBuilder sbCount = new StringBuilder();


            sbCount.Append("SELECT COUNT(*) FROM ( ");
            sbCount.Append("select d1.VoucherDate,WareHouseItem.Code,WareHouseItem.Name,d1.VoucherCode,Unit.UnitName, ");
            sbCount.Append("   COALESCE( ");
            sbCount.Append("   COALESCE(d1.Quantity,0) ");
            sbCount.Append("   +  ");
            sbCount.Append("   (SELECT ");
            sbCount.Append("   COALESCE(sum(id.Quantity),0) ");
            sbCount.Append("FROM (Inward i ");
            sbCount.Append("  JOIN InwardDetail id ");
            sbCount.Append("    ON ((i.Id = id.InwardId and i.OnDelete=0 and id.OnDelete=0 and id.ItemId=@pWareHouseItemId and i.WareHouseID  in @pWareHouseId   and i.VoucherDate <d1.VoucherDate))) ");
            sbCount.Append(")-(SELECT ");
            sbCount.Append("  COALESCE(sum(od.Quantity),0) ");
            sbCount.Append("FROM (Outward o ");
            sbCount.Append("  JOIN OutwardDetail od ");
            sbCount.Append("    ON ((o.Id = od.OutwardId and o.OnDelete=0 and od.OnDelete=0 and od.ItemId=@pWareHouseItemId and o.WareHouseID  in @pWareHouseId  and o.VoucherDate<d1.VoucherDate))) ");
            sbCount.Append("),0) as Beginning, ");
            sbCount.Append("  (SELECT ");
            sbCount.Append("  COALESCE(sum(id.Quantity),0) ");
            sbCount.Append("FROM (Inward i ");
            sbCount.Append("  JOIN InwardDetail id ");
            sbCount.Append("    ON ((i.Id = id.InwardId and i.OnDelete=0 and id.OnDelete=0 and id.ItemId=@pWareHouseItemId and i.WareHouseID  in @pWareHouseId   and i.VoucherDate=d1.VoucherDate))) ");
            sbCount.Append(") as Import, ");
            sbCount.Append("(SELECT ");
            sbCount.Append("  COALESCE(sum(od.Quantity),0) ");
            sbCount.Append("FROM (Outward o ");
            sbCount.Append("  JOIN OutwardDetail od ");
            sbCount.Append("    ON ((o.Id = od.OutwardId and o.OnDelete=0 and od.OnDelete=0 and od.ItemId=@pWareHouseItemId and o.WareHouseID  in @pWareHouseId   and o.VoucherDate=d1.VoucherDate ))) ");
            sbCount.Append(") as Export,d1.Reason,d1.EmployeeName,d1.DepartmentName,d1.ProjectName,d1.Description from (SELECT ");
            sbCount.Append("  bwh.ItemId AS ItemId, ");
            sbCount.Append("  COALESCE(bwh.Quantity,0) as Quantity, ");
            sbCount.Append("  bwh.CreatedDate as VoucherDate, ");
            sbCount.Append("   0 as Beginning, ");
            sbCount.Append(" 0 as Import, ");
            sbCount.Append("0 as Export, ");
            sbCount.Append("bwh.WareHouseId, ");
            sbCount.Append("'CDK' as VoucherCode, ");
            sbCount.Append("'' as Reason, ");
            sbCount.Append("'' as EmployeeName,'' as DepartmentName,'' as ProjectName,'' as Description ");
            sbCount.Append("FROM BeginningWareHouse bwh ");
            sbCount.Append("where bwh.OnDelete=0 and bwh.ItemId=@pWareHouseItemId  and bwh.WareHouseId  in @pWareHouseId   ");
            sbCount.Append("UNION all ");
            sbCount.Append("SELECT DISTINCT InwardDetail.ItemId as ItemId,0 as Quantity,Inward.VoucherDate as VoucherDate, 0 as Beginning,0 as Import,0 as Export,Inward.WareHouseID,Inward.VoucherCode as VoucherCode,Inward.Reason,InwardDetail.EmployeeName,InwardDetail.DepartmentName,InwardDetail.ProjectName,Inward.Description from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId where Inward.OnDelete=0 and InwardDetail.OnDelete=0 and InwardDetail.ItemId=@pWareHouseItemId and Inward.WareHouseID  in @pWareHouseId   ");
            sbCount.Append("UNION all ");
            sbCount.Append("SELECT DISTINCT OutwardDetail.ItemId as ItemId,0 as Quantity,Outward.VoucherDate as VoucherDate, 0 as Beginning,0 as Import,0 as Export,Outward.WareHouseID as WareHouseId, Outward.VoucherCode,Outward.Reason,OutwardDetail.EmployeeName,OutwardDetail.DepartmentName,OutwardDetail.ProjectName,Outward.Description from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId where Outward.OnDelete=0 and OutwardDetail.OnDelete=0 and OutwardDetail.ItemId=@pWareHouseItemId and Outward.WareHouseID  in @pWareHouseId  ) as d1 ");
            sbCount.Append("inner join WareHouseItem on WareHouseItem.Id=d1.ItemId ");
            sbCount.Append("inner join Unit on WareHouseItem.UnitId=Unit.Id ");
            sbCount.Append("where d1.VoucherDate BETWEEN @pFrom  AND @pTo ");
            if (!string.IsNullOrEmpty(request.KeySearch))
                sbCount.Append("and (d1.VoucherCode like @key or d1.DepartmentName like @key or d1.EmployeeName like @key or d1.Description like @key or d1.ProjectName like @key) ");
            sbCount.Append("group by d1.VoucherDate,d1.Quantity,d1.Beginning,d1.Import,d1.Export,WareHouseItem.Code,WareHouseItem.Name,d1.VoucherCode,Unit.UnitName,d1.Reason,d1.EmployeeName,d1.DepartmentName,d1.ProjectName,d1.Description ");
            sbCount.Append("              ) t   ");
            sbCount.Append("  ");

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@p2", request.Skip);
            parameter.Add("@p3", request.Take);
            parameter.Add("@pWareHouseId", departmentIds);
            parameter.Add("@pWareHouseItemId", request.WareHouseItemId);
            parameter.Add("@pFrom", ExtensionFull.GetDateToSqlRaw(request.FromDate));
            parameter.Add("@pTo", ExtensionFull.GetDateToSqlRaw(request.ToDate));
            Console.WriteLine(sb.ToString());
            Console.WriteLine(departmentIds);
            _list.Result = await _repository.GetList<ReportValueDetalsDT0>(sb.ToString(), parameter, commandType: CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, commandType: CommandType.Text);
            return _list;
        }


    }
}