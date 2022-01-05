using System.Collections.Generic;
using MediatR;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetAll.WareHouseItemCategory
{
    public class GetDropDownWareHouseItemCategoryCommand: IRequest<IEnumerable<WareHouseItemCategoryDTO>>
    {
        public bool Ative { get; set; } = true;
    }
}