﻿

using System;
using MediatR;
using WareHouse.API.Controllers.BaseController;
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
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.API.Application.Queries.GetAll.WareHouseItemCategory;
using WareHouse.API.Application.Queries.Paginated.WareHouseItemCategory;
using WareHouse.API.Application.Queries.Paginated.WareHouses;
using WareHouse.API.Application.Cache.CacheName;

namespace WareHouse.API.Controllers
{
    public class WareHouseItemCategoryController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;

        public WareHouseItemCategoryController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseItemCategoryCommand paginatedList)
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


        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownWareHouseItemCategoryCommand command)
        {
            command.CacheKey = string.Format(WareHouseItemCategoryCacheName.WareHouseItemCategoryDropDown, command.Active);
            command.BypassCache = true;
            var data = await _mediat.Send(command);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }

        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseItemCategoryCommands wareHouseCommands)
        {
            var res = await _mediat.Send(new UpdateWareHouseItemCategoryCommand() { WareHouseItemCategoryCommands = wareHouseCommands });
            // if (res)
            //     await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseItemCategoryCommands wareHouseCommands)
        {
            var data = await _mediat.Send(new CreateWareHouseItemCategoryCommand() { WareHouseItemCategoryCommands = wareHouseCommands });
            // if (data)
            //   await _cacheExtension.RemoveAllKeysBy(WareHouseItemCategoryCacheName.Prefix);
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
            var res = await _mediat.Send(new DeleteWareHouseItemCategoryCommand() { Id = listIds });
            // if (res)
            //   await _cacheExtension.RemoveAllKeysBy(WareHouseItemCategoryCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
    }
}