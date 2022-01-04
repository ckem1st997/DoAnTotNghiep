using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseItem
{
    public class PaginatedWareHouseItemCommand: IRequest<IPaginatedList<WareHouseItemDTO>>
    {
        public WareHouseItemSearchModel SearchModel { get; set; }
    }
}