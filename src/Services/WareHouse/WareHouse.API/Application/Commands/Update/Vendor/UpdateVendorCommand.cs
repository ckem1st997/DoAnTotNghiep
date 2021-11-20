using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateVendorCommand: IRequest<bool>
    {
        [DataMember]
        public VendorCommands VendorCommands { get; set; }
    }
}