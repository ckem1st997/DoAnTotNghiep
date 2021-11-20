using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.Paginated.Vendor
{
    public class PaginatedVendorCommand: IRequest<IPaginatedList<VendorDTO>>
    {
        [DataMember]
        public string KeySearch { get; set; }
        
        [DataMember]
        public int PageIndex { get; set; }
        [DataMember]
        public int PageNumber { get; set; }
    }
}