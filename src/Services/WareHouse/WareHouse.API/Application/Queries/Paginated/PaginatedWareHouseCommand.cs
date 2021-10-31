using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.Paginated
{
    [DataContract]
    public class PaginatedWareHouseCommand : IRequest<IPaginatedList<WareHouseDTO>>
    {
        [DataMember]
        public string KeySearch { get; set; }

        [DataMember]
        public decimal? fromPrice { get; set; }

        [DataMember]
        public decimal? toPrice { get; set; }
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageNumber { get; set; }
    }
}