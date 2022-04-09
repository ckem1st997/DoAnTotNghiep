using Dapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WareHouse.Domain.IRepositories;

namespace GrpcGetDataToWareHouse
{
  //  [Authorize]
    public class GrpcGetDataWareHouseService : GrpcGetDataWareHouse.GrpcGetDataWareHouseBase
    {

        private readonly IDapper _dapper;
        private readonly ILogger<GrpcGetDataWareHouseService> _logger;    
        public GrpcGetDataWareHouseService( IDapper dapper, ILogger<GrpcGetDataWareHouseService> logger)
        {
            _logger = logger;
            _dapper = dapper;
        }

        public override async Task<ListStringWareHouseId> GetListWarehouseById(BaseId request, ServerCallContext context)
        {

            //get list id Chidren
            var departmentIds = new List<string>();
            if (!string.IsNullOrEmpty(request.IdWareHouse))
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
                parameterwh.Add("@WareHouseId", request.IdWareHouse);
                departmentIds =
                    (List<string>)await _dapper.GetList<string>(GetListChidren.ToString(), parameterwh,
                        CommandType.Text);
                departmentIds.Add(request.IdWareHouse);

            }
            var result = new ListStringWareHouseId();
            if(departmentIds !=null && departmentIds.Count>0)
            foreach (var item in departmentIds)
            {
                result.IdWareHouseList = result.IdWareHouseList + "," + item;
            }
            return result;
        }
    }
}
