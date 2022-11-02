using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Share.BaseCore.Cache;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetAll.Unit
{
    public class GetDropDownUnitCommand: IRequest<IEnumerable<UnitDTO>>, ICacheableMediatrQuery
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