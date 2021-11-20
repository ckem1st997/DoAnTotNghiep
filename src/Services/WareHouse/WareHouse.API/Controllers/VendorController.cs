using System;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WareHouse.API.Application.Commands.Create.Vendor;
using WareHouse.API.Application.Commands.Models.Vendor;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.Paginated.Vendor;

namespace WareHouse.API.Controllers
{
    
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IMediator _mediat;

        public VendorController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedVendorCommand paginatedList)
        {
            var data = await _mediat.Send(new PaginatedVendorCommand() { KeySearch = paginatedList.KeySearch, PageNumber = paginatedList.PageNumber,  PageIndex = paginatedList.PageIndex });
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);

        }


        // [Route("edit")]
        // [HttpPost]
        // [ProducesResponseType((int)HttpStatusCode.OK)]
        // [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // public async Task<IActionResult> Edit(WareHouseCommands wareHouseCommands)
        // {
        //     var result = new ResultMessageResponse()
        //     {
        //         success = await _mediat.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands }),
        //     };
        //     return Ok(result);
        // }
        //
        //
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(VendorCommands vendorCommands)
        {
            var data = await _mediat.Send(new CreateVendorCommand() { VendorCommands = vendorCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
        //
        //
        // [Route("delete")]
        // [HttpPost]
        // [ProducesResponseType((int)HttpStatusCode.OK)]
        // [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // public async Task<IActionResult> Delete(string Id)
        // {
        //     var result = new ResultMessageResponse()
        //     {
        //         success = await _mediat.Send(new DeleteWareHouseCommand() { Id = Id })
        //     };
        //     return Ok(result);
        // }

    }
}
