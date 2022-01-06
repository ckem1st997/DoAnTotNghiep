using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Delete
{
    public partial class DeleteOutwardDetailCommand : IRequest<bool>
    {
        [DataMember] public IEnumerable<string> Id { get; set; }

    }
}
