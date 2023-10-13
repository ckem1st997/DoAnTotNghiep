using Microsoft.EntityFrameworkCore;
using Share.Base.Core.AutoDependencyInjection.InjectionAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Interface
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int totalCount { get; set; }

        public PaginatedList()
        {
            totalCount = 0;
        }


        public async Task Get(IQueryable<T> queryable, int skip, int take)
        {
            Result = await queryable.Skip(skip).Take(take).ToListAsync();
            totalCount = await queryable.CountAsync();
        }
    }

    public class PaginatedListDynamic
    {
        public dynamic Result { get; set; }

        public dynamic totalCount { get; set; }

        public PaginatedListDynamic()
        {
            Result = null;
            totalCount = 0;
        }
    }
}