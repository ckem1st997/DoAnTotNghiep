using System.Collections.Generic;
using MediatR;
using WareHouse.API.Application.Interface;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Application.Queries.GetAll.WareHouses
{
    public class GetDropDownWareHouseCommand: IRequest<IEnumerable<WareHouseDTO>>
    {
        public bool Ative { get; set; } = true;
    }
}