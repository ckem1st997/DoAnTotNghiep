using System.Runtime.Serialization;
using MediatR;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Commands.Create
{
    public partial class CreateWareHouseItemCategoryCommand : IRequest<bool>
    {
        [DataMember]
        public WareHouseItemCategoryCommands WareHouseItemCategoryCommands { get; set; }
    }
}
