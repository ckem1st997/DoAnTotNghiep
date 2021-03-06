using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateAuditDetailSerialCommand: IRequest<bool>
    {
        [DataMember]
        public AuditDetailSerialCommands AuditDetailSerialCommands { get; set; }
    }
}
