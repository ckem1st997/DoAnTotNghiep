using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateOutwardCommand : IRequest<bool>
    {
        [DataMember]
        public OutwardCommands OutwardCommands { get; set; }
    }
}
