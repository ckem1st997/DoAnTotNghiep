using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWarehouseBalanceCommand : IRequest<bool>
    {
        [DataMember]
        public WarehouseBalanceCommands WarehouseBalanceCommands { get; set; }
    }
}
