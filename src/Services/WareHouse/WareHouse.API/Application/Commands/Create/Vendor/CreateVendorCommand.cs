using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models.Vendor;
using WareHouse.API.Application.Commands.Models.WareHouse;

namespace WareHouse.API.Application.Commands.Create.Vendor
{
    public class CreateVendorCommand: IRequest<bool>
    {
        [DataMember]
        public VendorCommands VendorCommands { get; set; }
    }
}
