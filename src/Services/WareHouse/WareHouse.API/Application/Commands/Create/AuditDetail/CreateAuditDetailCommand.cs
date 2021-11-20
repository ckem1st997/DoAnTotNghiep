using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateAuditDetailCommand: IRequest<bool>
    {
        [DataMember]
        public AuditDetailCommands AuditDetailCommands { get; set; }
    }
}
