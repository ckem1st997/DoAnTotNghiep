using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{     
    public partial class UpdateUnitCommand: IRequest<bool>
    {
        [DataMember]
        public UnitCommands UnitCommands { get; set; }
    }
}
