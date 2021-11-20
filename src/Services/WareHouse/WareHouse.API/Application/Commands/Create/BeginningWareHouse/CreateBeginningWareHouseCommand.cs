using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateInwardCommand: IRequest<bool>
    {
        [DataMember]
        public BeginningWareHouseCommands BeginningWareHouseCommands { get; set; }
    }
}
