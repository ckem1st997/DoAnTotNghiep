using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Queries.Paginated.WareHouses
{
    [DataContract]
    public class PaginatedWareHouseCommand :BaseSearchModel, IRequest<PaginatedList<WareHouseDTO>>
    {
    }
}