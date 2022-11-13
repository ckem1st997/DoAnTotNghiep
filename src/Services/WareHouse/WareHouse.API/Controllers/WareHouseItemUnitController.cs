using Microsoft.AspNetCore.Mvc;
using System;
using MediatR;
using WareHouse.API.Controllers.BaseController;
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
using WareHouse.API.Application.Queries.GetAll.WareHouseItem;
using WareHouse.API.Application.Queries.Paginated.WareHouseItem;
using WareHouse.API.Application.Queries.Paginated.WareHouseItemCategory;
using WareHouse.API.Application.Queries.Paginated.WareHouses;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetAll.Unit;
using WareHouse.API.Application.Queries.GetAll;
using WareHouse.API.Application.Queries.GetAll.WareHouseItemCategory;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;
using WareHouse.API.Application.Queries.Paginated.WareHouseItemUnit;
using WareHouse.API.Application.Authentication;

namespace WareHouse.API.Controllers
{
    public class WareHouseItemUnitController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        public WareHouseItemUnitController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }
        #region R

        //[CheckRole(LevelCheck.CREATE)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync(string idItem)
        {
            if (string.IsNullOrEmpty(idItem))
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    success = false,
                    totalCount = 0
                };
                return Ok(res);
            }

            var search = new PaginatedWareHouseItemUnitCommand()
            {
                KeySearch = idItem
            };
            var data = await _mediat.Send(search);
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }


        //[CheckRole(LevelCheck.READ)]
        [Route("check-item-unit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckAsync(string idItem, string unitId)
        {
            if (string.IsNullOrEmpty(idItem) || string.IsNullOrEmpty(unitId))
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    success = false,
                    totalCount = 0
                };
                return Ok(res);
            }

            var search = new WareHouseItemUnitCheckExitsCommand()
            {
                ItemId = idItem,
                UnitId = unitId
            };
            var data = await _mediat.Send(search);
            var result = new ResultMessageResponse()
            {
                success = data,
            };
            return Ok(result);
        }


        #endregion
        #region CD

        //[CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseItemUnitCommands itemCommands)
        {
            var data = await _mediat.Send(new CreateWareHouseItemUnitCommand() { WareHouseItemUnitCommands = itemCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }

        //[CheckRole(LevelCheck.DELETE)]
        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            var res = await _mediat.Send(new DeleteWareHouseItemUnitCommand() { Id = listIds });
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion




    }
}