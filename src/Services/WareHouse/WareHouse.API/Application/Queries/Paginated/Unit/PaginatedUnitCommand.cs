using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Queries.Paginated.Unit
{
    public class PaginatedUnitCommand:BaseSearchModel, IRequest<IPaginatedList<UnitDTO>>
    {
    }
}