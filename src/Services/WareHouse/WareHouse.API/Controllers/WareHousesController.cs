using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.Paginated.WareHouses;

namespace WareHouse.API.Controllers
{
    // [Route($"api/{VesionApi.GetVisionToApi()}/[controller]")]
    // [Route("api/v1/[controller]")]
    //[ApiVersion("1.0")]
    //[ApiVersion("1.1")]
    [Route("api/v{v:apiVersion}/[controller]")]
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
            var data = await _mediat.Send(new PaginatedWareHouseCommand() { KeySearch = paginatedList.KeySearch, PageNumber = paginatedList.PageNumber,  PageIndex = paginatedList.PageIndex });
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);

        }


        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseCommands wareHouseCommands)
        {
            var result = new ResultMessageResponse()
            {
                success = await _mediat.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands }),
            };
            return Ok(result);
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseCommands wareHouseCommands)
        {
            var data = await _mediat.Send(new CreateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(string Id)
        {
            var result = new ResultMessageResponse()
            {
                success = await _mediat.Send(new DeleteWareHouseCommand() { Id = Id })
            };
            return Ok(result);
        }

    }
}
