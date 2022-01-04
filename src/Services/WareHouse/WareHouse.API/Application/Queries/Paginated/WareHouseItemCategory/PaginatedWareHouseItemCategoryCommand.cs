using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Queries.Paginated.WareHouseItemCategory
{
    [DataContract]
    public class PaginatedWareHouseItemCategoryCommand: IRequest<IPaginatedList<WareHouseItemCategoryDTO>>
    {
        public WareHouseItemCategorySearchModel SearchModel { get; set; }
    }
}