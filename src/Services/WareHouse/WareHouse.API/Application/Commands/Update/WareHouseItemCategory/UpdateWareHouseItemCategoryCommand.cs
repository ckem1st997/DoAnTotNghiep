using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Update
{
    public partial class UpdateWareHouseItemCategoryCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseItemCategoryCommands WareHouseItemCategoryCommands { get; set; }
    }
}
