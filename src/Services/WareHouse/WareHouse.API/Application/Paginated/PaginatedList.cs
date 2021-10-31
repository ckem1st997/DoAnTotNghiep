using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WareHouse.API.Application.Interface
{
    public class PaginatedList<T>:IPaginatedList<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int totalCount { get; set; }

        public PaginatedList()
        {
            Result = null;
            totalCount = 0;
        }
    }
}