using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models.WareHouse;
using WareHouse.API.Application.Commands.Update.WareHouses;
using WareHouse.API.Application.Queries.Paginated;

namespace WareHouse.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class WareHousesController : ControllerBase
    {
        private readonly IMediator _mediat;

        public WareHousesController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseCommand paginatedList)
        {
            return Ok(await _mediat.Send(new PaginatedWareHouseCommand() { KeySearch = paginatedList.KeySearch, PageNumber = paginatedList.PageNumber, fromPrice = paginatedList.fromPrice, toPrice = paginatedList.toPrice, PageIndex = paginatedList.PageIndex }));

        }


        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] WareHouseCommands wareHouseCommands)
        {

            return Ok(await _mediat.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands }));
        }

    }
}
