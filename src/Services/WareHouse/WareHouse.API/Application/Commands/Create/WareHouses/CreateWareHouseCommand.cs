using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseCommands WareHouseCommands { get; set; }
    }
}
