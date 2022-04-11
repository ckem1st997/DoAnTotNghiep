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
using Microsoft.AspNetCore.Authorization;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Controllers
{

    public class UnitController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        public UnitController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
        }
        #region R

        [CheckRole(LevelCheck.READ)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedUnitCommand paginatedList)
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



        [CheckRole(LevelCheck.READ)]
        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownUnitCommand command)
        {
            command.CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, command.Active);
            command.BypassCache = false;
            var data = await _mediat.Send(command);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }

        #endregion

        #region CUD


        [CheckRole(LevelCheck.READ)]
        [Route("details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var resError = new ResultMessageResponse()
                {
                    success = false,
                    message = "Chưa nhập Id của đơn vị tính !"
                };
                return Ok(resError);
            }
            var commandCheck = new UnitGetFirstCommand()
            {
                Id = id
            };
            var resc = await _mediat.Send(commandCheck);
            if (resc is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });

            var result = new ResultMessageResponse()
            {
                data = resc,
                success = resc != null
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create()
        {
            var mode = new UnitDTO()
            {
                Id = Guid.NewGuid().ToString()
            };
            var result = new ResultMessageResponse()
            {
                data = mode,
                success = mode != null
            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                var resError = new ResultMessageResponse()
                {
                    success = false,
                    message = "Chưa nhập Id của đơn vị tính !"
                };
                return Ok(resError);
            }
            var commandCheck = new UnitGetFirstCommand()
            {
                Id = Id
            };
            var data = await _mediat.Send(commandCheck);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null
            };
            return Ok(result);
        }








        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(UnitCommands unitCommands)
        {
            var data = await _mediat.Send(new UpdateUnitCommand() { UnitCommands = unitCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(UnitCacheName.Prefix);
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
        public async Task<IActionResult> Create(UnitCommands unitCommands)
        {
            var data = await _mediat.Send(new CreateUnitCommand() { UnitCommands = unitCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(UnitCacheName.Prefix);
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
            var data = await _mediat.Send(new DeleteUnitCommand() { Id = listIds });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(UnitCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
        #endregion
    }
}