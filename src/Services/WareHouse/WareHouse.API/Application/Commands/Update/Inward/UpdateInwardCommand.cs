using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateInwardCommand: IRequest<bool>
    {
        [DataMember]
        public InwardCommands InwardCommands { get; set; }
    }
}
