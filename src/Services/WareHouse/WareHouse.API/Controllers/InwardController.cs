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
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.SignalRService;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Controllers
{
    public class InwardController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        private readonly IFakeData _ifakeData;
        private readonly IUserSevice _userSevice;
        private readonly ISignalRService _signalRService;

        public InwardController(ISignalRService signalRService,IUserSevice userSevice, IFakeData ifakeData, IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
            _ifakeData = ifakeData ?? throw new ArgumentNullException(nameof(ifakeData));
            _userSevice = userSevice;
            _signalRService = signalRService;

        }
        #region R    

        /// <summary>
        /// Kiểm tra vật tư tồn tại trong kho
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        /// 

        [CheckRole(LevelCheck.READ)]
        [Route("check-item-exist")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckItemExist(string itemId, string warehouseId)
        {
            if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(warehouseId))
            {
                return Ok(new ResultMessageResponse
                {
                    success = false,
                    message = "Bạn chưa chọn vật tư hoặc kho !"
                });
            }
            if (!string.IsNullOrEmpty(warehouseId))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(warehouseId);
                if (!check)
                    return Unauthorized(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    });
            }
            var data = await _mediat.Send(new CheckItemAndWareHouseItemByOutWardCommand() { ItemId = itemId, WareHouseId = warehouseId });

            return Ok(new ResultMessageResponse
            {
                data = data,
                success = data
            });
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
                    message = "Chưa nhập Id của phiếu !"
                };
                return Ok(resError);
            }
            var data = await _mediat.Send(new InwardGetFirstCommand() { Id = id });
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.WareHouseId))
                {
                    var check = await _userSevice.CheckWareHouseIdByUser(data.WareHouseId);
                    if (!check)
                        return Ok(new ResultMessageResponse()
                        {
                            success = false,
                            message = "Bạnn không có quyền truy cập vào kho này !"
                        }); ;
                }
                await GetDataToDrop(data, true);
            }

            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null
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
            if (string.IsNullOrEmpty(id))
            {
                var resError = new ResultMessageResponse()
                {
                    success = false,
                    message = "Chưa nhập Id của phiếu !"
                };
                return Ok(resError);
            }
            var data = await _mediat.Send(new InwardGetFirstCommand() { Id = id });
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.WareHouseId))
                {
                    var check = await _userSevice.CheckWareHouseIdByUser(data.WareHouseId);
                    if (!check)
                        return Ok(new ResultMessageResponse()
                        {
                            success = false,
                            message = "Bạnn không có quyền truy cập vào kho này !"
                        }); ;
                }
                await GetDataToDrop(data);
            }
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
        public async Task<IActionResult> Edit(InwardCommands inwardCommands)
        {

            if (!string.IsNullOrEmpty(inwardCommands.WareHouseId))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(inwardCommands.WareHouseId);
                if (!check)
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    }); ;
            }
            inwardCommands.ModifiedDate = DateTime.Now;
            foreach (var item in inwardCommands.InwardDetails)
            {
                item.Amount = item.Uiquantity * item.Uiprice;
                int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = item.ItemId, IdUnit = item.UnitId });
                item.Quantity = convertRate * item.Uiquantity;
                item.Price = item.Amount;
            }
            var data = await _mediat.Send(new UpdateInwardCommand() { InwardCommands = inwardCommands });
            var mes = false;
            if(data)
            {
                var user =await _userSevice.GetUser();
                mes =   await _userSevice.CreateHistory(user.UserName,"Chỉnh sửa","vừa chỉnh sửa phiếu nhập kho có mã "+inwardCommands.VoucherCode+"!",false,inwardCommands.Id);

            }

            var result = new ResultMessageResponse()
            {
                success = data,
                data = mes
            };
            return Ok(result);
        }



        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string idWareHouse)
        {
            if (!string.IsNullOrEmpty(idWareHouse))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(idWareHouse);
                if (!check)
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    }); ;
            }
            var modelCreate = new InwardDTO();
            modelCreate.WareHouseId = idWareHouse;
            modelCreate.VoucherCode = ExtensionFull.GetVoucherCode("PN");
            await GetDataToDrop(modelCreate);
            var result = new ResultMessageResponse()
            {
                data = modelCreate,
                success = true
            };
            return Ok(result);
        }
        private async Task<InwardDTO> GetDataToDrop(InwardDTO res, bool details = false)
        {

            var getVendor = new VendorDropDownCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(VendorCacheName.VendorCacheNameDropDown, true)
            };
            var dataVendor = await _mediat.Send(getVendor);

            var getWareHouse = new GetDropDownWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, true)
            };
            var dataWareHouse = await _mediat.Send(getWareHouse);

            if (details)
            {
                res.WareHouseDTO = dataWareHouse.Where(x => x.Id.Equals(res.WareHouseId));
                res.VendorDTO = dataVendor.Where(x => x.Id.Equals(res.VendorId));
                var createBy = await _ifakeData.GetCreateBy();
                res.GetCreateBy = createBy.Where(x => x.Id.Equals(res.CreatedBy));
            }
            else
            {
                res.WareHouseDTO = dataWareHouse;
                res.VendorDTO = dataVendor;
                res.GetCreateBy = await _ifakeData.GetCreateBy();
            }

            return res;
        }


        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(InwardCommands inwardCommands)
        {
            if (!string.IsNullOrEmpty(inwardCommands.WareHouseId))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(inwardCommands.WareHouseId);
                if (!check)
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    }); ;
            }
            inwardCommands.CreatedDate = DateTime.Now;
            inwardCommands.ModifiedDate = DateTime.Now;
            foreach (var item in inwardCommands.InwardDetails)
            {
                item.Amount = item.Uiquantity * item.Uiprice;
                int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = item.ItemId, IdUnit = item.UnitId });
                item.Quantity = convertRate * item.Uiquantity;
                item.Price = item.Amount;
            }
            var data = await _mediat.Send(new CreateInwardCommand() { InwardCommands = inwardCommands });
            var mes = false;
            if (data)
            {
                var user = await _userSevice.GetUser();
                mes = await _userSevice.CreateHistory(user.UserName, "Tạo", "vừa tạo mới phiếu nhập kho có mã " + inwardCommands.VoucherCode + "!", false, inwardCommands.Id);

            }

            var result = new ResultMessageResponse()
            {
                success = data,
                data = mes
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
            var data = await _mediat.Send(new DeleteInwardCommand() { Id = listIds });
            var mes = false;
            if (data)
            {
                var user = await _userSevice.GetUser();
                mes = await _userSevice.CreateHistory(user.UserName, "Xóa", "vừa xóa phiếu nhập kho !", false, "");

            }

            var result = new ResultMessageResponse()
            {
                success = data,
                data = mes
            };
            return Ok(result);          
        }
        #endregion

    }
}