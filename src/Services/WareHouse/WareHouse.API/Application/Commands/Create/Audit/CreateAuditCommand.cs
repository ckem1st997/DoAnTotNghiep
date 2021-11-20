﻿using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateAuditCommand: IRequest<bool>
    {
        [DataMember]
        public AuditCommands AuditCommands { get; set; }
    }
}
