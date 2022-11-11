using Dapper;
using Microsoft.AspNetCore.Mvc;
using Share.BaseCore;
using Share.BaseCore.Authozire;
using System.Data;
using System.Text;

namespace Master.Controllers
{
    public class ListRoleController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;

        public ListRoleController(MasterdataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("get-list-key")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList()
        {
            List<string> strings = AuthozireListKey.GetAllKey();
            return Ok(new ResultMessageResponse()
            {
                data = strings,
                totalCount = strings.Count
            });
        }



        [HttpGet]
        [Route("get-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList(string appId)
        {
            var list = _context.ListRoles.Where(x => x.Id != null && x.AppId.Equals(appId));
            //  var res = GetWareHouseTreeModel(list);
            var res1 = GetWareHouseTree(2, list);
            //for (int i = 0; i < 5; i++)
            //{
            //    res1.AddRange(res1);
            //}
            return Ok(new ResultMessageResponse()
            {
                data = res1,
                totalCount = 10
            });
        }


        [HttpGet]
        [Route("get-list-tree")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetListTree(string appId)
        {
            var list = _context.ListRoles.Where(x => x.Id != null && x.AppId.Equals(appId));
            var res = GetWareHouseTreeModel(list);
            res.Add(new ListRole()
            {
                Id = "",
                Name = "Không có !"
            });

            return Ok(new ResultMessageResponse()
            {
                data = res,
                totalCount = 10
            });
        }

        class ListRoleTreeModel
        {
            public ListRoleTreeModel()
            {
                children = new List<ListRoleTreeModel>();
            }
            public ListRole Data { get; set; }
            public new IList<ListRoleTreeModel> children { get; set; }
        }
        IList<ListRoleTreeModel> GetWareHouseTree(int? expandLevel, IEnumerable<ListRole> ListRoles = null)
        {
            expandLevel ??= 1;
            var qq = new Queue<ListRoleTreeModel>();
            var lstCheck = new List<ListRoleTreeModel>();
            var result = new List<ListRoleTreeModel>();
            var convertToRoot = new List<ListRoleTreeModel>();
            var wareHouseModels = ListRoles;
            foreach (var s in wareHouseModels)
            {
                var tem = new ListRoleTreeModel
                {
                    children = new List<ListRoleTreeModel>(),
                    Data = new ListRole()
                    {
                        Id = s.Id,
                        Description = s.Description,
                        ParentId = s.ParentId,
                        Name = s.Name,
                        IsAPI = s.IsAPI,
                        Key = s.Key,
                        InActive = s.InActive,
                    }
                };
                convertToRoot.Add(tem);
            }

            var roots = convertToRoot
                .Where(w => string.IsNullOrEmpty(w.Data.ParentId))
                .OrderBy(o => o.Data.Name);

            foreach (var root in roots)
            {
                //root.level = 1;
                //root.expanded = root.level <= expandLevel.Value;
                //root.folder = true;
                qq.Enqueue(root);
                lstCheck.Add(root);
                result.Add(root);
            }

            while (qq.Any())
            {
                var cur = qq.Dequeue();
                if (lstCheck.All(a => a.Data.Id != cur.Data.Id))
                    result.Add(cur);

                var childs = convertToRoot
                    .Where(w => !string.IsNullOrEmpty(w.Data.ParentId) && w.Data.ParentId == cur.Data.Id)
                    .OrderBy(o => o.Data.Name);

                if (childs != null && !childs.Any())
                    continue;

                // var childLevel = cur.level + 1;
                foreach (var child in childs)
                {
                    if (lstCheck.Any(a => a.Data.Id == child.Data.Id))
                        continue;

                    //child.level = childLevel;
                    //child.expanded = !expandLevel.HasValue || child.level <= expandLevel.Value;

                    qq.Enqueue(child);
                    lstCheck.Add(child);
                    cur.children.Add(child);
                }
            }

            return result;
        }




        //class ListRoleTreeModel :ListRole
        //{
        //    public ListRoleTreeModel()
        //    {
        //        children = new List<ListRoleTreeModel>();
        //    }
        //    public new IList<ListRoleTreeModel> children { get; set; }
        //}
        //async Task<IList<ListRoleTreeModel>> GetWareHouseTree(int? expandLevel, IEnumerable<ListRole> ListRoles = null)
        //{
        //    expandLevel ??= 1;
        //    var qq = new Queue<ListRoleTreeModel>();
        //    var lstCheck = new List<ListRoleTreeModel>();
        //    var result = new List<ListRoleTreeModel>();
        //    var convertToRoot = new List<ListRoleTreeModel>();
        //    var wareHouseModels = ListRoles;
        //    foreach (var s in wareHouseModels)
        //    {
        //        var tem = new ListRoleTreeModel
        //        {
        //            children = new List<ListRoleTreeModel>(),
        //            Id = s.Id,
        //            Description = s.Description,
        //            ParentId = s.ParentId,
        //            Name = s.Name,
        //            IsAPI= s.IsAPI,
        //            Key = s.Key,    
        //            InActive= s.InActive,
        //        };
        //        convertToRoot.Add(tem);
        //    }

        //    var roots = convertToRoot
        //        .Where(w => string.IsNullOrEmpty(w.ParentId))
        //        .OrderBy(o => o.Name);

        //    foreach (var root in roots)
        //    {
        //        //root.level = 1;
        //        //root.expanded = root.level <= expandLevel.Value;
        //        //root.folder = true;
        //        qq.Enqueue(root);
        //        lstCheck.Add(root);
        //        result.Add(root);
        //    }

        //    while (qq.Any())
        //    {
        //        var cur = qq.Dequeue();
        //        if (lstCheck.All(a => a.Id != cur.Id))
        //            result.Add(cur);

        //        var childs = convertToRoot
        //            .Where(w => !string.IsNullOrEmpty(w.ParentId) && w.ParentId == cur.Id)
        //            .OrderBy(o => o.Name);

        //        if (childs != null && !childs.Any())
        //            continue;

        //       // var childLevel = cur.level + 1;
        //        foreach (var child in childs)
        //        {
        //            if (lstCheck.Any(a => a.Id == child.Id))
        //                continue;

        //            //child.level = childLevel;
        //            //child.expanded = !expandLevel.HasValue || child.level <= expandLevel.Value;

        //            qq.Enqueue(child);
        //            lstCheck.Add(child);
        //            cur.children.Add(child);
        //        }
        //    }

        //    return result;
        //}




        //

        private List<ListRole> GetWareHouseTreeModel(IEnumerable<ListRole> models)
        {
            var parents = models.Where(w => string.IsNullOrEmpty(w.ParentId));

            var result = new List<ListRole>();
            var level = 0;
            foreach (var parent in parents)
            {
                result.Add(new ListRole
                {
                    Id = parent.Id,
                    Name = parent.Name,
                });
                GetChildWareHouseTreeModel(ref models, parent.Id, ref result, level);
            }

            return result;
        }

        private void GetChildWareHouseTreeModel(ref IEnumerable<ListRole> models, string parentId,
            ref List<ListRole> result, int level)
        {
            level++;
            var childs = models.Where(w => w.ParentId.Equals(parentId));

            if (childs.Any())
            {
                foreach (var child in childs)
                {
                    result.Add(new ListRole()
                    {
                        Id = child.Id,
                        ParentId = child.ParentId,
                        Name = GetTreeLevelString(level) + child.Name
                    });
                    GetChildWareHouseTreeModel(ref models, child.Id, ref result, level);
                }
            }
        }

        public static string GetTreeLevelString(int level)
        {
            if (level <= 0)
                return "";

            var result = "";
            for (var i = 1; i <= level; i++)
            {
                result += "–";
            }

            return result;
        }

        [CheckRole(LevelCheck.CREATE)]
        [HttpGet]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create()
        {
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = new ListRole()
            });
        }


        [CheckRole(LevelCheck.CREATE)]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListRole list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            list.Id = Guid.NewGuid().ToString();
            await _context.ListRoles.AddAsync(list);
            return Ok(new ResultMessageResponse()
            {
                success = await _context.SaveChangesAsync() > 0
            });
        }



        [HttpGet]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    message = "Chưa nhập mã Id !"
                });
            }

            var res = await _context.ListRoles.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(new ResultMessageResponse()
            {
                success = res != null,
                data = res
            });
        }


        [HttpPost]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(ListRole list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListRoles.Update(list);
            var res = await _context.SaveChangesAsync();
            return Ok(new ResultMessageResponse()
            {
                success = res > 0
            });
        }



        [CheckRole(LevelCheck.DELETE)]
        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            bool res = false;
            var get = _context.ListRoles.Where(x => listIds.Contains(x.Id));
            if (get != null)
            {
                _context.ListRoles.RemoveRange(get);
                res = await _context.SaveChangesAsync() > 0;
            }

            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }

    }
}