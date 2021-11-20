using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWareHouseItemUnitCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseItemUnitCommands WareHouseItemUnitCommands { get; set; }
    }
}
