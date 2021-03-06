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
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetAll.WareHouseItem;
using WareHouse.API.Application.Authentication;


namespace WareHouse.API.Controllers
{
    [CheckRole(LevelCheck.WAREHOUSE)]
    public class BeginningWareHouseController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;

        public BeginningWareHouseController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        #region R

        [CheckRole(LevelCheck.READ)]
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

        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string idWareHouse)
        {
            var res = new BeginningWareHouseDTO()
            {
                WareHouseId = idWareHouse,
                Id = Guid.NewGuid().ToString()
            };
            await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
            {
                var resu = new ResultMessageResponse()
                {
                    success = false,
                    message = "Ch??a ch???n t???n kho !"
                };
                return Ok(resu);
            }

            var command = new BeginningWareHouseGetFirstCommand()
            {
                Id = id
            };
            var res = await _mediat.Send(command);
            await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }

        private async Task<BeginningWareHouseDTO> GetDataToDrop(BeginningWareHouseDTO res)
        {
            var getUnit = new GetDropDownUnitCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, true)
            };
            var dataUnit = await _mediat.Send(getUnit);

            var getWareHouse = new GetDropDownWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, true)
            };
            var dataWareHouse = await _mediat.Send(getWareHouse);

            var getWareHouseItem = new GetDopDownWareHouseItemCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCacheName.WareHouseItemCacheNameDropDown, true)
            };
            var dataWareHouseItem = await _mediat.Send(getWareHouseItem);

            res.UnitDTO = dataUnit;
            res.WareHouseDTO = dataWareHouse;
            res.WareHouseItemDTO = dataWareHouseItem;
            return res;
        }

        [CheckRole(LevelCheck.UPDATE)]

        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(BeginningWareHouseCommands command)
        {
            if (command is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Kh??ng t???n t???i t???n kho !"
                });
            var commandCheck = new BeginningWareHouseGetFirstCommand()
            {
                Id = command.Id
            };
            var res = await _mediat.Send(commandCheck);
            if (res is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Kh??ng t???n t???i t???n kho !"
                });
            command.CreatedDate = res.CreatedDate;
            command.ModifiedDate = DateTime.Now;
            var data = await _mediat.Send(
                new UpdateBeginningWareHouseCommand() { BeginningWareHouseCommands = command });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(BeginningWareHouseCommands command)
        {
            var check = await _mediat.Send(new BeginningCheckItemAndWareHouseCommand()
            {
                ItemId = command.ItemId,
                WareHouseId = command.WareHouseId
            });

            if (check)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "V???t t?? ???? c?? t???n trong kho !"
                });
            command.CreatedDate=DateTime.Now;
            var data = await _mediat.Send(
                new CreateBeginningWareHouseCommand() { BeginningWareHouseCommands = command });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }



        [CheckRole(LevelCheck.DELETE)]
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

