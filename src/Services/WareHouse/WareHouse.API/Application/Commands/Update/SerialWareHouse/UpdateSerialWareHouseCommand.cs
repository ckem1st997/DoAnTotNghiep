using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateSerialWareHouseCommand : IRequest<bool>
    {
        [DataMember]
        public SerialWareHouseCommands SerialWareHouseCommands { get; set; }
    }
}
