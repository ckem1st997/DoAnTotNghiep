using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWareHouseItemCommand: IRequest<bool>
    {
        [DataMember]
        public WareHouseItemCommands WareHouseItemCommands { get; set; }
    }
}
