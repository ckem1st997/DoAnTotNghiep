using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Queries.Paginated.Vendor
{
    public class PaginatedVendorCommand:BaseSearchModel, IRequest<IPaginatedList<VendorDTO>>
    {
    }
}