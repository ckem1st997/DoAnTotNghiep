using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateOutwardDetailCommand : IRequest<bool>
    {
        [DataMember]
        public OutwardDetailCommands OutwardDetailCommands { get; set; }
    }
}
