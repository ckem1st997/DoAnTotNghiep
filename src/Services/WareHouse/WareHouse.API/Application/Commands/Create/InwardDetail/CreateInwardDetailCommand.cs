using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateInwardDetailCommand : IRequest<bool>
    {
        [DataMember]
        public InwardDetailCommands InwardDetailCommands { get; set; }
    }
}
