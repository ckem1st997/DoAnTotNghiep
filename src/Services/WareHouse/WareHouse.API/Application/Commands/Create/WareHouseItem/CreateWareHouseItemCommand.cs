using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseItemCommand: IRequest<bool>
    {
        [DataMember]
        public WareHouseItemCommands WareHouseItemCommands { get; set; }
    }
}
