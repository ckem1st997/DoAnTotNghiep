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

namespace WareHouse.API.Controllers
{
    public class InwardController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        public InwardController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
        }
        #region R      
        #endregion

        #region CUD

        #endregion

        [Route("edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var resError= new ResultMessageResponse()
                {
                    success = false,
                    message="Chưa nhập Id của phiếu !"
                };
                return Ok(resError);
            }
            var data = await _mediat.Send(new InwardGetFirstCommand() { Id = id });
            var result = new ResultMessageResponse()
            {
               data= data,
               success=data !=null
            };
            return Ok(result);
        }


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

        [Route("create")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(string idWareHouse)
        {
            var modelCreate = new InwardDTO();
            modelCreate.Voucher = new Random().Next(1, 999999999).ToString();
            modelCreate.WareHouseId = idWareHouse;
            await GetDataToDrop(modelCreate);
            var result = new ResultMessageResponse()
            {
                data = modelCreate,
                success = true
            };
            return Ok(result);
        }
        private async Task<InwardDTO> GetDataToDrop(InwardDTO res)
        {

            var getVendor = new VendorDropDownCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(VendorCacheName.VendorCacheNameDropDown, true)
            };
            var dataVendor = await _mediat.Send(getVendor);

            var getWareHouseItemCategory = new GetDropDownWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCategoryCacheName.WareHouseItemCategoryDropDown, true)
            };
            var dataWareHouseItemCategory = await _mediat.Send(getWareHouseItemCategory);
            res.WareHouseDTO = dataWareHouseItemCategory;
            res.VendorDTO = dataVendor;
            res.GetCreateBy = FakeData.GetCreateBy();
            return res;
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(InwardCommands inwardCommands)
        {
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
            var data = await _mediat.Send(new DeleteUnitCommand() { Id = listIds });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(UnitCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
    }
}