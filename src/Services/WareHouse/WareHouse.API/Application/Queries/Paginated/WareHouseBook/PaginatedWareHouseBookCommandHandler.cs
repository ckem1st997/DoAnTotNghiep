using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using StackExchange.Profiling.Internal;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseBook
{
    public static class TypeWareHouseBook
    {
        public const string In = "Phiếu nhập";
        public const string Out = "Phiếu xuất";
    }

    public class PaginatedWareHouseBookCommand : BaseSearchModel, IRequest<IPaginatedList<WareHouseBookDTO>>
    {
        public string TypeWareHouseBook { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
        
        public string WareHouseId { get; set; }

    }

    public class PaginatedWareHouseBookCommandHandler : IRequestHandler<PaginatedWareHouseBookCommand,
        IPaginatedList<WareHouseBookDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<WareHouseBookDTO> _list;

        public PaginatedWareHouseBookCommandHandler(IDapper repository, IPaginatedList<WareHouseBookDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public async Task<IPaginatedList<WareHouseBookDTO>> Handle(PaginatedWareHouseBookCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim() ?? "";
            request.TypeWareHouseBook = request.TypeWareHouseBook?.Trim() ?? "";
            StringBuilder sb = new StringBuilder();
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM (  ");
            sbCount.Append("select d1.Id from  ");
            sbCount.Append(
                "(select Inward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu nhập ' as Type,Inward.OnDelete from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId ");
            sbCount.Append("union all  ");
            sbCount.Append(
                "select Outward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu xuất' as Type,Outward.OnDelete from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId ) d1 ");
            sbCount.Append(" where ");


            sb.Append("select d1.*,WareHouse.Name as WareHouseName from  ");
            sb.Append(
                "(select Inward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu nhập ' as Type,Inward.OnDelete from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId ");
            sb.Append("union all  ");
            sb.Append(
                "select Outward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu xuất' as Type,Outward.OnDelete from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId ) d1 ");
            sb.Append(" inner join WareHouse on d1.WareHouseID=WareHouse.Id ");
            sb.Append(" where ");
            if (request.FromDate.HasValue)
            {
                sb.Append(" d1.VoucherDate>=@fromDate and ");
                sbCount.Append(" d1.VoucherDate>=@fromDate and ");
            }

            if (request.ToDate.HasValue)
            {
                sb.Append(" d1.VoucherDate<=@toDate and ");
                sbCount.Append(" d1.VoucherDate<=@toDate and ");
            }

            if (request.KeySearch.HasValue())
            {
                sb.Append(" (d1.Reason like @key or d1.VoucherCode like @key) and ");
                sbCount.Append(" (d1.Reason like @key or d1.VoucherCode like @key) and ");
            }
            
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

            
            if (request.WareHouseId.HasValue() && departmentIds.Count() > 0)
            {
                sb.Append(" d1.WareHouseId in @WareHouseId and ");
                sbCount.Append(" d1.WareHouseId in @WareHouseId and ");
            }
            if (request.TypeWareHouseBook.HasValue() && (request.TypeWareHouseBook == TypeWareHouseBook.In ||
                                                         request.TypeWareHouseBook == TypeWareHouseBook.Out))
            {
                sb.Append(" d1.Type=@type and ");
                sbCount.Append(" d1.Type=@type and ");
            }

            sb.Append("    ");
            sb.Append(" d1.OnDelete=0 order by d1.VoucherDate desc ");
            sb.Append("OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");

            sbCount.Append("    ");
            sbCount.Append(" d1.OnDelete=0 ");
            sbCount.Append(" ) t   ");
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@key", '%' + request.KeySearch + '%');
            parameter.Add("@type", request.TypeWareHouseBook);
            parameter.Add("@WareHouseId", departmentIds);
            parameter.Add("@fromDate", request.FromDate);
            parameter.Add("@toDate", request.ToDate);
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            _list.Result = await _repository.GetList<WareHouseBookDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(sbCount.ToString(), parameter, CommandType.Text);
            return _list;
        }
    }
}