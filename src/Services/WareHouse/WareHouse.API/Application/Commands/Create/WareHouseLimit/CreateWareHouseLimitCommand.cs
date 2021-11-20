using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseLimitCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseLimitCommands WareHouseLimitCommands { get; set; }
    }
}
