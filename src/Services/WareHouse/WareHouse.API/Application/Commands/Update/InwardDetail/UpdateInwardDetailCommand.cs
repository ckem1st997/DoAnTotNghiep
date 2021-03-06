using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateInwardDetailCommand : IRequest<bool>
    {
        [DataMember]
        public InwardDetailCommands InwardDetailCommands { get; set; }
    }
}
