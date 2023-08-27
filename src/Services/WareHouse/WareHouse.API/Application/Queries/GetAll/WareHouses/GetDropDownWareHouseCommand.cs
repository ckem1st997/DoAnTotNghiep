using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Share.Base.Core.Cache;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetAll.WareHouses
{
    public class GetDropDownWareHouseCommand: IRequest<IEnumerable<WareHouseDTO>>, ICacheableMediatrQuery
    {
        public bool Active { get; set; } = true;
        [BindNever]
        public bool BypassCache { get; set; }
        [BindNever]
        public string CacheKey { get; set;}
        [BindNever]
        public TimeSpan? SlidingExpiration { get;set; }
    }
}