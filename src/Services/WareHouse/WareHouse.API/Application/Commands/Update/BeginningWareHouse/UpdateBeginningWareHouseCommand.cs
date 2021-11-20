using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateBeginningWareHouseCommand : IRequest<bool>
    {
        [DataMember]
        public BeginningWareHouseCommands BeginningWareHouseCommands { get; set; }
    }
}
