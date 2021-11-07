using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models.WareHouse;

namespace WareHouse.API.Application.Commands.Create.WareHouses
{
    public class CreateWareHouseCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseCommands WareHouseCommands { get; set; }
    }
}
