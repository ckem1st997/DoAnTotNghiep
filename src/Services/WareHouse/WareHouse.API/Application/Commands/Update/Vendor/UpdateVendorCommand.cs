using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models.Vendor;
using WareHouse.API.Application.Commands.Models.WareHouse;

namespace WareHouse.API.Application.Commands.Update.Vendor
{
    public class UpdateVendorCommand: IRequest<bool>
    {
        [DataMember]
        public VendorCommands VendorCommands { get; set; }
    }
}