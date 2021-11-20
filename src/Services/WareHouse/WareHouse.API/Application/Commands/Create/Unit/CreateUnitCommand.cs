using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{     
    public partial class UpdateUnitCommand: IRequest<bool>
    {
        [DataMember]
        public UnitCommands UnitCommands { get; set; }
    }
}
