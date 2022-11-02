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
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;


namespace WareHouse.API.Application.Queries.Paginated
{
    public class PaginatedBeginningWareHouseCommand : BaseSearchModel, IRequest<IPaginatedList<BeginningWareHouseDTO>>
    {
        public string WareHouseId { get; set; }
    }

    public class PaginatedBeginningWareHouseCommandHandler : IRequestHandler<PaginatedBeginningWareHouseCommand,
        IPaginatedList<BeginningWareHouseDTO>>
    {
        private readonly IDapper _repository;
        private readonly IPaginatedList<BeginningWareHouseDTO> _list;
        public readonly IUserSevice _context;

        public PaginatedBeginningWareHouseCommandHandler(IUserSevice context, IDapper repository, IPaginatedList<BeginningWareHouseDTO> list)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _list = list ?? throw new ArgumentNullException(nameof(list));
            _context = context;
        }

        public async Task<IPaginatedList<BeginningWareHouseDTO>> Handle(PaginatedBeginningWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            request.KeySearch = request.KeySearch?.Trim() ?? "";
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("SELECT COUNT(*) FROM ( ");
            sbCount.Append(" select BeginningWareHouse.Id from BeginningWareHouse ");
            sbCount.Append(" join WareHouse on BeginningWareHouse.WareHouseId=WareHouse.Id and WareHouse.OnDelete=0 ");
            sbCount.Append(
                " join WareHouseItem on BeginningWareHouse.ItemId=WareHouseItem.Id and WareHouseItem.OnDelete=0 ");
            sbCount.Append(" join Unit on BeginningWareHouse.UnitId=Unit.Id and Unit.OnDelete=0 ");
            sbCount.Append(" where ");
            StringBuilder sb = new StringBuilder();
            sb.Append(
                "select BeginningWareHouse.Id,WareHouse.Name as WareHouseName,Unit.UnitName,WareHouseItem.Name as ItemName,BeginningWareHouse.Quantity from BeginningWareHouse ");
            sb.Append(" join WareHouse on BeginningWareHouse.WareHouseId=WareHouse.Id and WareHouse.OnDelete=0 ");
            sb.Append(
                " join WareHouseItem on BeginningWareHouse.ItemId=WareHouseItem.Id and WareHouseItem.OnDelete=0 ");
            sb.Append(" join Unit on BeginningWareHouse.UnitId=Unit.Id and Unit.OnDelete=0 ");
            sb.Append(" where ");
            if (!string.IsNullOrEmpty(request.KeySearch))
            {
                sb.Append("  (WareHouse.Name like @key or WareHouseItem.Name like @key) and ");
                sbCount.Append("  (WareHouse.Name like @key or WareHouseItem.Name like @key) and ");
            }
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
                    (List<string>)await _repository.GetList<string>(GetListChidren.ToString(), parameterwh,
                        CommandType.Text);
                departmentIds.Add(request.WareHouseId);
                if (user.RoleNumber < 3)
                    departmentIds = departmentIds.Where(x => user.WarehouseId.Contains(x)).ToList();
            }

            //
            if (!string.IsNullOrEmpty(request.WareHouseId) && departmentIds.Count() > 0 || user.RoleNumber < 3)
            {
                sb.Append("  BeginningWareHouse.WareHouseId in @WareHouseId and ");
                sbCount.Append("  BeginningWareHouse.WareHouseId in @WareHouseId and ");
            }

            sb.Append("  BeginningWareHouse.OnDelete=0 ");
            sbCount.Append("  BeginningWareHouse.OnDelete=0 ");
            //
            sbCount.Append(" ) t   ");
            sb.Append(" order by UnitName OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ");
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
            parameter.Add("@skip", request.Skip);
            parameter.Add("@take", request.Take);
            _list.Result = await _repository.GetList<BeginningWareHouseDTO>(sb.ToString(), parameter, CommandType.Text);
            _list.totalCount = await _repository.GetAyncFirst<int>(ValidatorString.GetSqlCount(sb.ToString(), SqlEnd: "order"), parameter, CommandType.Text);
            return _list;
        }
    }
}