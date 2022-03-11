using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateSerialWareHouseCommand : IRequest<bool>
    {
        [DataMember]
        public IEnumerable<SerialWareHouseCommands> SerialWareHouseCommands { get; set; }
    }
}
