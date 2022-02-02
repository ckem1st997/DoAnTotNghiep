﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WareHouse.API.Application.Cache;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.Domain.IRepositories;

namespace WareHouse.API.Application.Queries.GetAll.WareHouses
{
    public class GetTreeWareHouseCommand : IRequest<IEnumerable<WareHousesTreeModel>>, ICacheableMediatrQuery
    {
        public bool Active { get; set; }
        [BindNever]
        public bool BypassCache { get; set; }
        [BindNever]
        public string CacheKey { get; set; }
        [BindNever]
        public TimeSpan? SlidingExpiration { get; set; }
    }

    public class
        GetTreeWareHouseCommandHandler : IRequestHandler<GetTreeWareHouseCommand, IEnumerable<WareHousesTreeModel>>
    {
        private readonly IDapper _repository;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _rep;

        public GetTreeWareHouseCommandHandler(IDapper repository, IRepositoryEF<Domain.Entity.WareHouse> rep)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rep = rep ?? throw new ArgumentNullException(nameof(rep));
        }

        public async Task<IEnumerable<WareHousesTreeModel>> Handle(GetTreeWareHouseCommand request,
            CancellationToken cancellationToken)
        {
            if (request == null)
                return null;
            return await GetWareHouseTree(2,request.Active);
        }

        public virtual async Task<IList<WareHousesTreeModel>> GetWareHouseTree(int? expandLevel,
            bool showHidden = false)
        {
            expandLevel ??= 1;
            var qq = new Queue<WareHousesTreeModel>();
            var lstCheck = new List<WareHousesTreeModel>();
            var result = new List<WareHousesTreeModel>();
            var convertToRoot = new List<WareHousesTreeModel>();
            var wareHouseModels = await GetOrganizationalUnits(showHidden);
            //   organizationalUnitModels = organizationalUnitModels.ToList();
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


        private async Task<IList<WareHouseDTO>> GetOrganizationalUnits(bool showHidden = false)
        {
            // var cacheKey = WarehouseCacheKeys.Warehouses.AllCacheKey.FormatWith(showHidden);
            // var models = _cacheManager.GetDb(cacheKey, () =>
            // {
            //     var result = _organizationalUnitService.GetAll(showHidden);
            //     return result.ToList();
            // });
            var models = await _rep.ListAllAsync();
            var res = new List<WareHouseDTO>();
            var wareHouses = models.ToList();
            if (wareHouses?.Count() > 0)
            {
                foreach (var x in wareHouses)
                {
                    if (x.Inactive.Equals(showHidden))
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
            }

            return res;
        }
    }
}