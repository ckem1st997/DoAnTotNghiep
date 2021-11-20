using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWareHouseLimitCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseLimitCommands WareHouseLimitCommands { get; set; }
    }
}
