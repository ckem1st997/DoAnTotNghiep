using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.GetAll.Unit;
using WareHouse.API.Application.Queries.Paginated.Unit;
using WareHouse.API.Controllers.BaseController;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Queries.Paginated;

namespace WareHouse.API.Controllers
{
    public class BeginningWareHouseController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        public BeginningWareHouseController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }
        #region R
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedBeginningWareHouseCommand paginatedList)
        {
            var data = await _mediat.Send(paginatedList);
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }
        #endregion

        #region CUD
        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(UpdateBeginningWareHouseCommand command)
        {
            var data = await _mediat.Send(new UpdateBeginningWareHouseCommand() { BeginningWareHouseCommands = command.BeginningWareHouseCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateBeginningWareHouseCommand command)
        {
            var data = await _mediat.Send(new CreateBeginningWareHouseCommand() { BeginningWareHouseCommands = command.BeginningWareHouseCommands });
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
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteBeginningWareHouseCommand() { Id = listIds });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
        #endregion



    }
}