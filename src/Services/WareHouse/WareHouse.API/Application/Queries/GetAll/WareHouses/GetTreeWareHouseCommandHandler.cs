using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Share.BaseCore.Extensions;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.Entity;
using Share.BaseCore.BaseNop;

namespace WareHouse.API.Application.Queries.GetAll.WareHouses
{
    public class GetTreeWareHouseCommand : IRequest<IEnumerable<WareHousesTreeModel>>
    {
        public bool Active { get; set; }
        public bool GetAll { get; set; } = false;

        public IEnumerable<WareHouseDTO> WareHouseDTOs { get; set; }
    }

    public class
        GetTreeWareHouseCommandHandler : IRequestHandler<GetTreeWareHouseCommand, IEnumerable<WareHousesTreeModel>>
    {
        private readonly IDapper _repository;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _rep;
        public readonly IUserSevice _context;

        public GetTreeWareHouseCommandHandler(IUserSevice context, IDapper repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rep = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            _context = context;
        }

        public async Task<IEnumerable<WareHousesTreeModel>> Handle(GetTreeWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            return await GetWareHouseTree(2, request.Active, request.WareHouseDTOs, request.GetAll);
        }

        public virtual async Task<IList<WareHousesTreeModel>> GetWareHouseTree(int? expandLevel,
            bool showHidden = false, IEnumerable<WareHouseDTO> WareHouseDTOs = null, bool GetAll = false)
        {
            expandLevel ??= 1;
            var qq = new Queue<WareHousesTreeModel>();
            var lstCheck = new List<WareHousesTreeModel>();
            var result = new List<WareHousesTreeModel>();
            var convertToRoot = new List<WareHousesTreeModel>();
            var user = await _context.GetUser();
            if (string.IsNullOrEmpty(user.WarehouseId) && user.RoleNumber < 3)
                return new List<WareHousesTreeModel>();
            var wareHouseModels = await GetOrganizationalUnits(showHidden, WareHouseDTOs, GetAll);
            foreach (var s in wareHouseModels)
            {
                var tem = new WareHousesTreeModel
                {
                    children = new List<WareHousesTreeModel>(),
                    folder = false,
                    key = s.Id,
                    title = s.Name,
                    tooltip = s.Name,
                    Path = s.Path,
                    ParentId = s.ParentId,
                    Code = s.Code,
                    Name = s.Name
                };
                convertToRoot.Add(tem);
            }

            var roots = convertToRoot
                .Where(w => string.IsNullOrEmpty(w.ParentId))
                .OrderBy(o => o.Name);

            foreach (var root in roots)
            {
                root.level = 1;
                root.expanded = root.level <= expandLevel.Value;
                root.folder = true;
                qq.Enqueue(root);
                lstCheck.Add(root);
                result.Add(root);
            }

            while (qq.Any())
            {
                var cur = qq.Dequeue();
                if (lstCheck.All(a => a.key != cur.key))
                    result.Add(cur);

                var childs = convertToRoot
                    .Where(w => !string.IsNullOrEmpty(w.ParentId) && w.ParentId.ToString() == cur.key)
                    .OrderBy(o => o.Name);

                if (childs != null && !childs.Any())
                    continue;

                var childLevel = cur.level + 1;
                foreach (var child in childs)
                {
                    if (lstCheck.Any(a => a.key == child.key))
                        continue;

                    child.level = childLevel;
                    child.expanded = !expandLevel.HasValue || child.level <= expandLevel.Value;

                    qq.Enqueue(child);
                    lstCheck.Add(child);
                    cur.children.Add(child);
                }
            }

            return result;
        }


        private async Task<IList<WareHouseDTO>> GetOrganizationalUnits(bool showHidden = false, IEnumerable<WareHouseDTO> WareHouseDTOs = null, bool GetAll = false)
        {
            //string sql = "select Id,ParentId,Code,Name,Path from WareHouse where Inactive =@active and OnDelete=0 ";
            //DynamicParameters parameter = new DynamicParameters();
            //parameter.Add("@active", showHidden ? 1 : 0);
            //var models = await _repository.GetAllAync<WareHouseDTO>(sql, parameter, CommandType.Text);
            var wareHouses = WareHouseDTOs.ToList();
            //get list id Chidren
            if (!GetAll)
            {
                var user = await _context.GetUser();

                if (!string.IsNullOrEmpty(user.WarehouseId))
                {
                    var split = user.WarehouseId.Split(',');
                    if (split.Length > 0)
                    {
                        var ress = "";
                        for (int i = 0; i < split.Length; i++)
                        {
                            if (i == split.Length - 1)
                                ress = ress + "'" + split[i] + "'";
                            else
                                ress = ress + "'" + split[i] + "'" + ",";
                        }
                        user.WarehouseId = ress;

                    }
                }
                var departmentIds = new List<string>();
                if (user != null && !string.IsNullOrEmpty(user.WarehouseId) && user.RoleNumber < 3)
                {
                    StringBuilder GetListChidren = new StringBuilder();
                    GetListChidren.Append("with cte (Id, Name, ParentId) as ( ");
                    GetListChidren.Append("  select     wh.Id, ");
                    GetListChidren.Append("             wh.Name, ");
                    GetListChidren.Append("             wh.ParentId ");
                    GetListChidren.Append("  from       WareHouse wh ");
                    GetListChidren.Append("  where       ( wh.ParentId in (" + user.WarehouseId + ") or  wh.Id in (" + user.WarehouseId + ") ) and  wh.OnDelete=0 ");
                    GetListChidren.Append("  union all ");
                    GetListChidren.Append("  SELECT     p.Id, ");
                    GetListChidren.Append("             p.Name, ");
                    GetListChidren.Append("             p.ParentId ");
                    GetListChidren.Append("  from       WareHouse  p  ");
                    GetListChidren.Append("  inner join cte ");
                    GetListChidren.Append("          on p.Id = cte.ParentId where p.OnDelete=0 ");
                    GetListChidren.Append(") ");
                    GetListChidren.Append(" select cte.Id FROM cte GROUP BY cte.Id,cte.Name,cte.ParentId; ");
                    DynamicParameters parameterwh = new DynamicParameters();
                    Console.WriteLine(GetListChidren.ToString());
                    departmentIds =
                        (List<string>)await _repository.GetList<string>(GetListChidren.ToString(), null,
                            CommandType.Text);
                    wareHouses = WareHouseDTOs.Where(x => departmentIds.Contains(x.Id)).ToList();

                }

            }


            var res = new List<WareHouseDTO>();
            if (wareHouses?.Count > 0)
            {
                foreach (var x in wareHouses)
                {
                    var m = new WareHouseDTO
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Name = x.Name,
                        ParentId = x.ParentId,
                        Path = x.Path
                    };
                    res.Add(m);
                }
            }

            return res;
        }
    }
}