using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateWarehouseBalanceCommand : IRequest<bool>
    {
        [DataMember]
        public WarehouseBalanceCommands WarehouseBalanceCommands { get; set; }
    }
}
