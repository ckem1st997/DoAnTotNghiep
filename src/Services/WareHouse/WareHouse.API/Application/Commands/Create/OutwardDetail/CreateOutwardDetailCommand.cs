using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateOutwardDetailCommand : IRequest<bool>
    {
        [DataMember]
        public OutwardDetailCommands OutwardDetailCommands { get; set; }
    }
}
