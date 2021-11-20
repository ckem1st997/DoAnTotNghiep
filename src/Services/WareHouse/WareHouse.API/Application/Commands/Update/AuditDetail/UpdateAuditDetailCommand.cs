using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateAuditDetailCommand: IRequest<bool>
    {
        [DataMember]
        public AuditDetailCommands AuditDetailCommands { get; set; }
    }
}
