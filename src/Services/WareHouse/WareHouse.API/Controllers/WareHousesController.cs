using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;
using WareHouse.API.Application.Queries.Paginated.WareHouses;
using WareHouse.API.Controllers.BaseController;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Model;

namespace WareHouse.API.Controllers
{
    public class WareHousesController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;

        public WareHousesController(IMediator mediat, ICacheExtension cacheExtension)
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
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseCommand paginatedList)
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
        [Route("get-by-id")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] WareHouseGetFirstCommand firstCommand)
        {
            var data = await _mediat.Send(firstCommand);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.READ)]
        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownWareHouseCommand paginatedList)
        {
            paginatedList.CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, paginatedList.Active);
            paginatedList.BypassCache = false;
            var data = await _mediat.Send(paginatedList);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.READ)]
        [Route("get-all")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediat.Send(new GetAllWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, true)
            });
            var result = new ResultMessageResponse()
            {
                data = list
            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.READ)]
        [Route("get-tree-view")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeViewAsync([FromQuery] GetTreeWareHouseCommand paginatedList)
        {
            var list = await _mediat.Send(new GetAllWareHouseCommand()
            {
                Active = paginatedList.Active,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, paginatedList.Active)
            });
            paginatedList.WareHouseDTOs = list;
            var data = await _mediat.Send(paginatedList);
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
                    message = "Chưa nhập Id của kho !"
                };
                return Ok(resError);
            }
            var commandCheck = new WareHouseGetFirstCommand()
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
            await GetDataToDrop(resc);
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
        public async Task<IActionResult> CreateAsync()
        {
            var mode = new WareHouseDTO()
            {
                Id = Guid.NewGuid().ToString()
            };
            await GetDataToDrop(mode);
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
                    message = "Chưa nhập Id của kho !"
                };
                return Ok(resError);
            }
            var commandCheck = new WareHouseGetFirstCommand()
            {
                Id = Id
            };
            var data = await _mediat.Send(commandCheck);

            await GetDataToDrop(data);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null
            };
            return Ok(result);
        }



        private async Task<WareHouseDTO> GetDataToDrop(WareHouseDTO res)
        {
            var getWareHouse = new GetDropDownWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCategoryCacheName.WareHouseItemCategoryDropDown, true)
            };
            var dataWareHouse = await _mediat.Send(getWareHouse);
            res.WareHouseDTOs = dataWareHouse;
            return res;
        }



        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseCommands wareHouseCommands)
        {
            if (wareHouseCommands is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });
            var commandCheck = new WareHouseGetFirstCommand()
            {
                Id = wareHouseCommands.Id
            };
            var resc = await _mediat.Send(commandCheck);
            if (resc is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });
            var res = await _mediat.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
            if (res)
                await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseCommands wareHouseCommands)
        {
            var check = await _mediat.Send(new WareHouseCodeCommand()
            {
                Code = wareHouseCommands.Code.Trim()
            });
            if (check)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Mã đã tồn tại, xin vui lòng chọn mã khác !"
                });
            }
            var data = await _mediat.Send(new CreateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
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
            var res = await _mediat.Send(new DeleteWareHouseCommand() { Id = listIds });
            if (res)
                await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion
    }
}