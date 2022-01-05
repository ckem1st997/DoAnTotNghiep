﻿using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateOutwardCommand : IRequest<bool>
    {
        [DataMember]
        public OutwardCommands OutwardCommands { get; set; }
    }
}
