using Dapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
//using Microsoft.Identity.Web;
using Share.Base.Service.Repository;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace GrpcGetDataToWareHouse
{
    [Authorize]
    public class GrpcGetDataWareHouseService : GrpcGetDataWareHouse.GrpcGetDataWareHouseBase
    {

        private readonly IRepositoryEF<WareHouse.Domain.Entity.WareHouse> _dapper;
        private readonly ILogger<GrpcGetDataWareHouseService> _logger;
        public GrpcGetDataWareHouseService(IRepositoryEF<WareHouse.Domain.Entity.WareHouse> dapper , ILogger<GrpcGetDataWareHouseService> logger)
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
                if (!string.IsNullOrEmpty(request.IdWareHouse))
                {
                    var split = request.IdWareHouse.Split(',');
                    if (split.Length > 0)
                    {
                        var res = "";
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (i == split.Length - 1)
                                res = res + "'" + split[i] + "'";
                            else
                                res = res + "'" + split[i] + "'" + ",";
                        }
                        request.IdWareHouse = res;

                    }
                }

                StringBuilder GetListChidren = new StringBuilder();
                GetListChidren.Append("with cte (Id, Name, ParentId) as ( ");
                GetListChidren.Append("  select     wh.Id, ");
                GetListChidren.Append("             wh.Name, ");
                GetListChidren.Append("             wh.ParentId ");
                GetListChidren.Append("  from       WareHouse wh ");
                GetListChidren.Append("  where     ( wh.ParentId in (" + request.IdWareHouse + ") or  wh.Id in (" + request.IdWareHouse + ") ) and  wh.OnDelete=0 ");
                GetListChidren.Append("  union all ");
                GetListChidren.Append("  SELECT     p.Id, ");
                GetListChidren.Append("             p.Name, ");
                GetListChidren.Append("             p.ParentId ");
                GetListChidren.Append("  from       WareHouse  p  ");
                GetListChidren.Append("  inner join cte ");
                GetListChidren.Append("          on  p.ParentId = cte.id where p.OnDelete=0 ");
                GetListChidren.Append(") ");
                GetListChidren.Append(" select cte.Id FROM cte GROUP BY cte.Id,cte.Name,cte.ParentId; ");


                DynamicParameters parameterwh = new DynamicParameters();
                parameterwh.Add("@WareHouseId", request.IdWareHouse);
                departmentIds =
                    (List<string>)await _dapper.QueryAsync<string>(GetListChidren.ToString(), parameterwh,
                        CommandType.Text);
                //    departmentIds.Add(request.IdWareHouse);

            }
            int iii = 0;
            var result = new ListStringWareHouseId();
            if (departmentIds != null && departmentIds.Count > 0)
                foreach (var item in departmentIds)
                {
                    if (iii == 0)
                        result.IdWareHouseList = item;
                    else
                        result.IdWareHouseList = result.IdWareHouseList + "," + item;
                    iii++;
                }
            return result;
        }
    }
}
