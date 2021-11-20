using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateVendorCommand: IRequest<bool>
    {
        [DataMember]
        public VendorCommands VendorCommands { get; set; }
    }
}
