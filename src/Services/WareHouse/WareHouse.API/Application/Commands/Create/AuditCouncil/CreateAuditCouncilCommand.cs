﻿using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class UpdateAuditCouncilCommand: IRequest<bool>
    {
        [DataMember]
        public AuditCouncilCommands AuditCouncilCommands { get; set; }
    }
}
