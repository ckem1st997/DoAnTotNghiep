

using System;
using MediatR;
using WareHouse.API.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.API.Application.Queries.GetAll.WareHouseItemCategory;
using WareHouse.API.Application.Queries.Paginated.WareHouseItemCategory;
using WareHouse.API.Application.Queries.Paginated.WareHouses;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Model;
using Share.BaseCore.Cache.CacheName;

namespace WareHouse.API.Controllers
{
    public class WareHouseItemCategoryController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        public WareHouseItemCategoryController(IMediator mediat, ICacheExtension cacheExtension)
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

        [CheckRole(LevelCheck.READ)]
        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownWareHouseItemCategoryCommand command)
        {
            command.CacheKey = string.Format(WareHouseItemCategoryCacheName.WareHouseItemCategoryDropDown, command.Active);
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
                    message = "Chưa nhập Id của loại vật tư !"
                };
                return Ok(resError);
            }
            var commandCheck = new WareHouseItemCategoryFirstCommand()
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
            if (resc != null)
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
        public IActionResult Create()
        {
            var mode = new WareHouseItemCategoryDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Code = ExtensionFull.GetVoucherCode("WHI")

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
                    message = "Chưa nhập Id của loại vật tư !"
                };
                return Ok(resError);
            }
            var commandCheck = new WareHouseItemCategoryFirstCommand()
            {
                Id = Id
            };
            var data = await _mediat.Send(commandCheck);
            if (data != null)
                await GetDataToDrop(data);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null
            };
            return Ok(result);
        }




        private async Task<WareHouseItemCategoryDTO> GetDataToDrop(WareHouseItemCategoryDTO res)
        {

            var command = new GetDropDownWareHouseItemCategoryCommand();
            command.CacheKey = string.Format(WareHouseItemCategoryCacheName.WareHouseItemCategoryDropDown, command.Active);
            command.BypassCache = false;
            var data = await _mediat.Send(command);
            res.InverseParent = (ICollection<WareHouseItemCategoryDTO>)data;
            return res;
        }




        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseItemCategoryCommands wareHouseCommands)
        {
            if (wareHouseCommands is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });
            var commandCheck = new WareHouseItemCategoryFirstCommand()
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

            var res = await _mediat.Send(new UpdateWareHouseItemCategoryCommand() { WareHouseItemCategoryCommands = wareHouseCommands });
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
        public async Task<IActionResult> Create(WareHouseItemCategoryCommands wareHouseCommands)
        {
            var check = await _mediat.Send(new WareHouseItemCategotyCodeCommand()
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
            var data = await _mediat.Send(new CreateWareHouseItemCategoryCommand() { WareHouseItemCategoryCommands = wareHouseCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(WareHouseItemCategoryCacheName.Prefix);
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
            var res = await _mediat.Send(new DeleteWareHouseItemCategoryCommand() { Id = listIds });
            if (res)
                await _cacheExtension.RemoveAllKeysBy(WareHouseItemCategoryCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion




    }
}