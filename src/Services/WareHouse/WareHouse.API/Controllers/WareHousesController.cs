using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Create.WareHouses;
using WareHouse.API.Application.Commands.Delete.WareHouses;
using WareHouse.API.Application.Commands.Models.WareHouse;
using WareHouse.API.Application.Commands.Update.WareHouses;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Queries.Paginated;

namespace WareHouse.API.Controllers
{
   // [Route($"api/{VesionApi.GetVisionToApi()}/[controller]")]
    [Route("api/v1/[controller]")]
    //  [Authorize]
    [ApiController]
    public class WareHousesController : ControllerBase
    {
        private readonly IMediator _mediat;

        public WareHousesController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseCommand paginatedList)
        {
            return Ok(await _mediat.Send(new PaginatedWareHouseCommand() { KeySearch = paginatedList.KeySearch, PageNumber = paginatedList.PageNumber, fromPrice = paginatedList.fromPrice, toPrice = paginatedList.toPrice, PageIndex = paginatedList.PageIndex }));

        }


        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseCommands wareHouseCommands)
        {

            return Ok(await _mediat.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands }));
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseCommands wareHouseCommands)
        {

            return Ok(await _mediat.Send(new CreateWareHouseCommand() { WareHouseCommands = wareHouseCommands }));
        } 
        
        
        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string Id)
        {

            return Ok(await _mediat.Send(new DeleteWareHouseCommand() { Id = Id }));
        }

    }
}
