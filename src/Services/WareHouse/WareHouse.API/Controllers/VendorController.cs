using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetAll;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Queries.Paginated.Vendor;
using WareHouse.API.Application.Validations;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{
    public class VendorController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;

        private readonly ICacheExtension _cacheExtension;
        public VendorController(IMediator mediat, ICacheExtension cacheExtension)
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
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedVendorCommand paginatedList)
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
        public async Task<IActionResult> GetTreeAsync([FromQuery] VendorDropDownCommand command)
        {
            command.CacheKey = string.Format(VendorCacheName.VendorCacheNameDropDown, command.Active);
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
                    message = "Chưa nhập Id của nhà cung cấp !"
                };
                return Ok(resError);
            }
            var commandCheck = new VendorGetFirstCommand()
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
            var mode = new VendorDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Code = ExtensionFull.GetVoucherCode("NCC")

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
                    message = "Chưa nhập Id của nhà cung cấp !"
                };
                return Ok(resError);
            }
            var commandCheck = new VendorGetFirstCommand()
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
        public async Task<IActionResult> Edit(VendorCommands vendorCommands)
        {
            var result = new ResultMessageResponse();
            result.success = await _mediat.Send(new UpdateVendorCommand() { VendorCommands = vendorCommands });
            if (result.success)
                await _cacheExtension.RemoveAllKeysBy(VendorCacheName.Prefix);
            return Ok(result);
        }

        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(VendorCommands vendorCommands)
        {
            var check = await _mediat.Send(new VendorCodeCommand()
            {
                Code = vendorCommands.Code.Trim()
            });
            if (check)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Mã đã tồn tại, xin vui lòng chọn mã khác !"
                });
            }
            var data = await _mediat.Send(new CreateVendorCommand() { VendorCommands = vendorCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(VendorCacheName.Prefix);
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
            var res = await _mediat.Send(new DeleteVendorCommand() { Id = listIds });
            if (res)
                await _cacheExtension.RemoveAllKeysBy(VendorCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion

    }
}