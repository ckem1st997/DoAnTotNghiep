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
using Share.BaseCore.Cache.CacheName;

namespace WareHouse.API.Controllers
{

    public class WareHouseItemController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        public WareHouseItemController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension;
        }
        #region R

        [CheckRole(LevelCheck.READ)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseItemCommand paginatedList)
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
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDopDownWareHouseItemCommand command)
        {
            command.CacheKey = string.Format(WareHouseItemCacheName.WareHouseItemCacheNameDropDown, command.Active);
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
            if (id is null)
            {
                var resu = new ResultMessageResponse()
                {
                    success = false,
                    message = "Chưa chọn mã !"
                };
                return Ok(resu);
            }

            var command = new WareHouseItemFirstCommand()
            {
                Id = id
            };
            var res = await _mediat.Send(command);
            await GetDataToDrop(res);
            var search = new PaginatedWareHouseItemUnitCommand()
            {
                KeySearch = id
            };
            var data = await _mediat.Send(search);
            res.WareHouseItemUnits = (ICollection<WareHouseItemUnitDTO>)data.Result;
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseItemCommands itemCommands)
        {
            if (itemCommands is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });
            var commandCheck = new WareHouseItemFirstCommand()
            {
                Id = itemCommands.Id
            };
            var res = await _mediat.Send(commandCheck);
            if (res is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });

            var data = await _mediat.Send(new UpdateWareHouseItemCommand() { WareHouseItemCommands = itemCommands });
            if (data)
            {
                await _cacheExtension.RemoveAllKeysBy(WareHouseItemCacheName.Prefix);
                //CreateOrUpdateWareHouseItemUnitCommand
                if (itemCommands.wareHouseItemUnits != null && itemCommands.wareHouseItemUnits.Count() > 0)
                {
                    var createUnitItem = new CreateOrUpdateWareHouseItemUnitCommand()
                    {
                        wareHouseItemUnitCommands = itemCommands.wareHouseItemUnits
                    };
                    await _mediat.Send(createUnitItem);
                }
            }
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create()
        {
            var res = new WareHouseItemDTO();
            res.Code =ExtensionFull.GetVoucherCode("ITEM");

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
                    message = "Chưa chọn mã !"
                };
                return Ok(resu);
            }

            var command = new WareHouseItemFirstCommand()
            {
                Id = id
            };
            var res = await _mediat.Send(command);
            await GetDataToDrop(res);
            var search = new PaginatedWareHouseItemUnitCommand()
            {
                KeySearch = id
            };
            var data = await _mediat.Send(search);
            res.WareHouseItemUnits = (ICollection<WareHouseItemUnitDTO>)data.Result;
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }

        private async Task<WareHouseItemDTO> GetDataToDrop(WareHouseItemDTO res)
        {
            var getUnit = new GetDropDownUnitCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, true)
            };
            var dataUnit = await _mediat.Send(getUnit);

            var getVendor = new VendorDropDownCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(VendorCacheName.VendorCacheNameDropDown, true)
            };
            var dataVendor = await _mediat.Send(getVendor);

            var getWareHouseItemCategory = new GetDropDownWareHouseItemCategoryCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCategoryCacheName.WareHouseItemCategoryDropDown, true)
            };
            var dataWareHouseItemCategory = await _mediat.Send(getWareHouseItemCategory);

            res.CategoryDTO = dataWareHouseItemCategory;
            res.VendorDTO = dataVendor;
            res.UnitDTO = dataUnit;
            return res;
        }

        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseItemCommands itemCommands)
        {
            var check = await _mediat.Send(new WareHouseItemCodeCommand()
            {
                Code = itemCommands.Code.Trim()
            });
            if (check)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Mã đã tồn tại, xin vui lòng chọn mã khác !"
                });
            }
            var data = await _mediat.Send(new CreateWareHouseItemCommand() { WareHouseItemCommands = itemCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(WareHouseItemCacheName.Prefix);
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
            var res = await _mediat.Send(new DeleteWareHouseItemCommand() { Id = listIds });
            if (res)
                await _cacheExtension.RemoveAllKeysBy(WareHouseItemCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion




    }
}