using System.Collections.Generic;
using MediatR;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetAll.Unit
{
    public class GetDropDownUnitCommand: IRequest<IEnumerable<UnitDTO>>
    {
        public bool Ative { get; set; } = true;
    }
}