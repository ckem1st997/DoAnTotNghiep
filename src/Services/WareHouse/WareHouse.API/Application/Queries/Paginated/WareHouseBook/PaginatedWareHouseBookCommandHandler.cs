
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Nest;
using Share.Base.Service.Repository;
using StackExchange.Profiling.Internal;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
namespace WareHouse.API.Application.Queries.Paginated.WareHouseBook
{
    public static class TypeWareHouseBook
    {
        public const string In = "Phiếu nhập";
        public const string Out = "Phiếu xuất";
    }

    public class PaginatedWareHouseBookCommand : BaseSearchModel, MediatR.IRequest<IPaginatedList<WareHouseBookDTO>>
    {
        public string TypeWareHouseBook { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string WareHouseId { get; set; }

    }

    public class PaginatedWareHouseBookCommandHandler : IRequestHandler<PaginatedWareHouseBookCommand,
        IPaginatedList<WareHouseBookDTO>>
    {
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IPaginatedList<WareHouseBookDTO> _list;
        public readonly IUserSevice _context;
        private readonly IElasticClient _elasticClient;


        public PaginatedWareHouseBookCommandHandler(IUserSevice context, IPaginatedList<WareHouseBookDTO> list, IElasticClient elasticClient, IRepositoryEF<Domain.Entity.WareHouse> repository)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));
            _context = context;
            _elasticClient = elasticClient;
            _repository = repository;
        }

        public async Task<IPaginatedList<WareHouseBookDTO>> Handle(PaginatedWareHouseBookCommand request,
            CancellationToken cancellationToken)
        {

            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim() ?? "";
            request.TypeWareHouseBook = request.TypeWareHouseBook?.Trim() ?? "";

            var user = await _context.GetUser();
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
                    (List<string>)await _repository.QueryAsync<string>(GetListChidren.ToString(), parameterwh,
                        CommandType.Text);
                departmentIds.Add(request.WareHouseId);
                if (user.RoleNumber < 3)
                    departmentIds = departmentIds.Where(x => user.WarehouseId.Contains(x)).ToList();
            }

            // sẽ search lâu vì sẽ dính exception
            //  var check = await _elasticClient.PingAsync();
            bool check = false;
            if (check)
            {
                #region Elastic

                // elastic sẽ mặc định trả về 10000 kết quả phù hợp nhất với yêu cầu tìm kiếm
                var queryContainers = new List<QueryContainer>();
                var descriptor = new QueryContainerDescriptor<WareHouseBookDTO>();
                if (!string.IsNullOrEmpty(request.KeySearch))
                {
                    queryContainers.Add(
                        descriptor.Term(t => t.WareHouseName, request.KeySearch)
                        || descriptor.Term(t => t.Reason, request.KeySearch)
                        || descriptor.Term(t => t.Description, request.KeySearch)
                        || descriptor.Term(t => t.Deliver, request.KeySearch)
                        || descriptor.Match(mq => mq.Field(f => f.WareHouseName).Query(request.KeySearch))
                        || descriptor.Match(mq => mq.Field(f => f.Reason).Query(request.KeySearch))
                        || descriptor.Match(mq => mq.Field(f => f.Description).Query(request.KeySearch))
                        || descriptor.Match(mq => mq.Field(f => f.Deliver).Query(request.KeySearch))
                        );
                }
                if (request.FromDate.HasValue)
                {
                    queryContainers.Add(descriptor.DateRange(x => x.Field(v => v.VoucherDate).GreaterThanOrEquals(request.FromDate)));
                }

                if (request.ToDate.HasValue)
                {
                    queryContainers.Add(descriptor.DateRange(x => x.Field(v => v.VoucherDate).LessThanOrEquals(request.ToDate)));
                }

                if (request.TypeWareHouseBook.Equals(TypeWareHouseBook.In) || request.TypeWareHouseBook.Equals(TypeWareHouseBook.Out))
                {
                    queryContainers.Add(descriptor.MatchPhrase(x => x.Field(v => v.Type).Query(request.TypeWareHouseBook)));
                }

                if (!request.WareHouseId.HasValue() && user.RoleNumber < 3)
                {
                    var list = new List<string>();
                    var split = user.WarehouseId.Split(',').ToList();
                    if (split.Any())
                    {
                        QueryContainer q = GetQueryByWareHouseId(split, descriptor);
                        queryContainers.Add(q);
                    }

                }
                else
                {
                    QueryContainer q = GetQueryByWareHouseId(departmentIds, descriptor);
                    queryContainers.Add(q);
                }


                var searchRequest = new SearchDescriptor<WareHouseBookDTO>();
                searchRequest.From(request.Skip).Size(request.Take).Query(q => q.Bool(b => b.Must(queryContainers.ToArray()))).Sort(x => x.Descending(y => y.VoucherDate));
                var res = await _elasticClient.SearchAsync<WareHouseBookDTO>(searchRequest);
                if (res.Hits != null)
                {
                    var listget = new List<WareHouseBookDTO>();
                    foreach (var item in res.Hits)
                    {
                        listget.Add(item.Source);
                    }
                    _list.Result = listget;
                }
                var countRequest = new CountDescriptor<WareHouseBookDTO>();
                countRequest.Query(q => q.Bool(b => b.Must(queryContainers.ToArray())));
                var resCount = await _elasticClient.CountAsync(countRequest);
                _list.totalCount = (int)resCount.Count;

                #endregion

            }

            else
            {
                #region Sql


                //sql
                StringBuilder sb = new StringBuilder();
                StringBuilder sbCount = new StringBuilder();
                //query count
                sbCount.Append("SELECT COUNT(*) FROM (  ");
                sbCount.Append("select d1.Id from  ");
                sbCount.Append(
                    "(select Viewer, Inward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu nhập' as Type,Inward.OnDelete from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId ");
                sbCount.Append(" where ");
                if (request.FromDate.HasValue)
                    sbCount.Append(" Inward.VoucherDate>=@fromDate and ");
                if (request.ToDate.HasValue)
                    sbCount.Append(" Inward.VoucherDate<=@toDate and ");
                if (request.KeySearch.HasValue())
                    sbCount.Append(" (Inward.Reason like @key or Inward.VoucherCode like @key) and ");
                if (request.WareHouseId.HasValue() && departmentIds.Count() > 0 || user.RoleNumber < 3)
                    sbCount.Append(" Inward.WareHouseId in @WareHouseId and ");
                sbCount.Append(" Inward.OnDelete=0  ");




                sbCount.Append("union all  ");
                sbCount.Append(
                    "select Viewer, Outward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu xuất' as Type,Outward.OnDelete from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId  ");
                sbCount.Append(" where ");
                if (request.FromDate.HasValue)
                    sbCount.Append(" Outward.VoucherDate>=@fromDate and ");
                if (request.ToDate.HasValue)
                    sbCount.Append(" Outward.VoucherDate<=@toDate and ");
                if (request.KeySearch.HasValue())
                    sbCount.Append(" (Outward.Reason like @key or Outward.VoucherCode like @key) and ");
                if (request.WareHouseId.HasValue() && departmentIds.Count() > 0 || user.RoleNumber < 3)
                    sbCount.Append(" Outward.WareHouseId in @WareHouseId and ");
                sbCount.Append(" Outward.OnDelete=0  ");
                sbCount.Append(" ) d1  ");


                #region querySelect
                //query select

                sb.Append(" select  top(" + request.Take + ") d1.*,WareHouse.Name as WareHouseName from  ");
                //query inward
                sb.Append(
                    "(select Viewer, Inward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu nhập' as Type,Inward.OnDelete from Inward inner join InwardDetail on Inward.Id=InwardDetail.InwardId  ");

                sb.Append(" where ");
                if (request.FromDate.HasValue)
                    sb.Append(" Inward.VoucherDate>=@fromDate and ");
                if (request.ToDate.HasValue)
                    sb.Append(" Inward.VoucherDate<=@toDate and ");
                if (request.KeySearch.HasValue())
                    sb.Append(" (Inward.Reason like @key or Inward.VoucherCode like @key) and ");
                if (request.WareHouseId.HasValue() && departmentIds.Count() > 0 || user.RoleNumber < 3)
                    sb.Append(" Inward.WareHouseId in @WareHouseId and ");
                sb.Append(" Inward.OnDelete=0 order by Inward.VoucherDate desc  OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY  ");
                sb.Append("union all  ");
                ////query outward
                sb.Append(
                    " select Viewer, Outward.Id,WareHouseId, VoucherCode,VoucherDate,CreatedBy,ModifiedBy,Deliver,Receiver,Reason,N'Phiếu xuất' as Type,Outward.OnDelete from Outward inner join OutwardDetail on Outward.Id=OutwardDetail.OutwardId  ");
                sb.Append(" where ");
                if (request.FromDate.HasValue)
                    sb.Append(" Outward.VoucherDate>=@fromDate and ");
                if (request.ToDate.HasValue)
                    sb.Append(" Outward.VoucherDate<=@toDate and ");
                if (request.KeySearch.HasValue())
                    sb.Append(" (Outward.Reason like @key or Outward.VoucherCode like @key) and ");
                if (request.WareHouseId.HasValue() && departmentIds.Count() > 0 || user.RoleNumber < 3)
                    sb.Append(" Outward.WareHouseId in @WareHouseId and ");
                sb.Append(" Outward.OnDelete=0 order by Outward.VoucherDate desc  OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY  ");
                sb.Append(" ) d1 ");
                sb.Append(" inner join WareHouse on d1.WareHouseID=WareHouse.Id ");

                if (request.TypeWareHouseBook.Equals(TypeWareHouseBook.In) || request.TypeWareHouseBook.Equals(TypeWareHouseBook.Out))
                    sb.Append(" where d1.Type=N'" + (request.TypeWareHouseBook.Equals(TypeWareHouseBook.In) ? TypeWareHouseBook.In : TypeWareHouseBook.Out) + "' ");
                //sb.Append("  and  ");
                //sb.Append(" d1.OnDelete=0  ");
                sb.Append(" group by d1.Viewer,d1.Id,d1.WareHouseID,d1.CreatedBy,d1.Deliver,d1.Reason,d1.Type,d1.VoucherCode,d1.VoucherDate,d1.ModifiedBy,d1.Receiver,d1.OnDelete,WareHouse.Name ");
                sb.Append("  order by d1.VoucherDate desc  ");
                //  sb.Append(" OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
                #endregion


                sbCount.Append("  inner join WareHouse on d1.WareHouseID=WareHouse.Id    ");
                if (request.TypeWareHouseBook.Equals(TypeWareHouseBook.In) || request.TypeWareHouseBook.Equals(TypeWareHouseBook.Out))
                    sbCount.Append(" where d1.Type=N'" + (request.TypeWareHouseBook.Equals(TypeWareHouseBook.In) ? TypeWareHouseBook.In : TypeWareHouseBook.Out) + "' ");
                sbCount.Append(" ");
                sbCount.Append(" group by d1.Viewer, d1.Id,d1.WareHouseID,d1.CreatedBy,d1.Deliver,d1.Reason,d1.Type,d1.VoucherCode,d1.VoucherDate,d1.ModifiedBy,d1.Receiver,d1.OnDelete ");
                sbCount.Append(" ) t   ");
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@key", '%' + request.KeySearch + '%');

                if (!request.WareHouseId.HasValue() && user.RoleNumber < 3)
                {
                    var list = new List<string>();
                    var split = user.WarehouseId.Split(',');
                    if (split.Length > 0)
                    {
                        for (int i = 0; i < split.Length; i++)
                        {
                            list.Add(split[i]);
                        }
                    }
                    parameter.Add("@WareHouseId", list);

                }
                else
                    parameter.Add("@WareHouseId", departmentIds);

                parameter.Add("@fromDate", request.FromDate);
                parameter.Add("@toDate", request.ToDate);
                parameter.Add("@skip", request.Skip);
                parameter.Add("@take", request.Take);
                _list.Result = await _repository.QueryAsync<WareHouseBookDTO>(sb.ToString(), parameter, CommandType.Text);
                _list.totalCount = await _repository.QueryFirstOrDefaultAsync<int>(sbCount.ToString(), parameter, CommandType.Text) - 1;
                //     var results = await Task.WhenAll(_list);

                #endregion
            }









            return _list;
        }

        private static QueryContainer GetQueryByWareHouseId(List<string> departmentIds, QueryContainerDescriptor<WareHouseBookDTO> descriptor)
        {
            QueryContainer q = new QueryContainer();
            foreach (var item in departmentIds)
            {
                q |= descriptor.Match(x => x.Field(v => v.WareHouseId).Query(item));
            }

            return q;
        }

    }
}