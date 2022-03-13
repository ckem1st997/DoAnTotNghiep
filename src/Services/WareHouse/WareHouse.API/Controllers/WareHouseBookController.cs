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
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouseItem;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Aspose.Cells;
using WareHouse.API.Application.Queries.GetFisrt;

namespace WareHouse.API.Controllers
{
    public class WareHouseBookController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WareHouseBookController(IWebHostEnvironment hostEnvironment, IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _hostingEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        }
        #region R
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseBookCommand paginatedList)
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


        [Route("get-unit-by-id")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUnitByIdAsync(string IdItem)
        {
            var data = await _mediat.Send(new GetWareHouseUnitByIdItemCommand() { IdItem = IdItem });
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = 1
            };
            return Ok(result);
        }

        #endregion

        #region CUD

        #endregion
        [Route("create-inward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create()
        {
            var res = new InwardDetailDTO();
            await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }

        [Route("edit-inward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditInwardDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }
            var res = await _mediat.Send(new InwardDetailsGetFirstCommand() { Id = id });
            if (res != null)
                await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }



        [Route("details-inward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DetailsInwardDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var result1 = new ResultMessageResponse()
                {
                    message = "Id is null or empty"
                };
                return Ok(result1);
            }
            var res = await _mediat.Send(new InwardDetailsGetFirstCommand() { Id = id });
            if (res != null)
                await GetDataToDrop(res, true);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }


        [Route("edit-inward-details")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditInwardDetails(InwardDetailCommands inwardDetailCommands)
        {
            if (inwardDetailCommands is null)
            {
                throw new ArgumentNullException(nameof(inwardDetailCommands));
            }

            var res = await _mediat.Send(new UpdateInwardDetailCommand() { InwardDetailCommands = inwardDetailCommands });
            if (res)
            {
                var listEntity = await _mediat.Send(new GetSerialByIdInwardDetailsCommand() { Id = inwardDetailCommands.Id });
                if (listEntity.Count() == 0 && inwardDetailCommands.SerialWareHouses != null)
                {
                    await _mediat.Send(new CreateSerialWareHouseCommand() { SerialWareHouseCommands = inwardDetailCommands.SerialWareHouses });
                }
                else if (listEntity.Count() > 0 && inwardDetailCommands.SerialWareHouses == null)
                {
                    var listIds = new List<string>();
                    foreach (var item in inwardDetailCommands.SerialWareHouses)
                    {
                        listIds.Add(item.Id);
                        await _mediat.Send(new DeleteSerialWareHouseCommand() { Ids = listIds });

                    }

                }
                else if (listEntity.Count() > 0 && inwardDetailCommands.SerialWareHouses != null)
                {

                    var listIds = new List<string>();
                    foreach (var item in listEntity)
                    {
                        var check = true;
                        foreach (var item2 in inwardDetailCommands.SerialWareHouses)
                        {
                            if (item.Id.Equals(item2.Id))
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                            listIds.Add(item.Id);
                    }
                    if (listIds.Count > 0)
                        await _mediat.Send(new DeleteSerialWareHouseCommand() { Ids = listIds });

                    //add
                    var listApps = new List<SerialWareHouseCommands>();
                    foreach (var item in inwardDetailCommands.SerialWareHouses)
                    {
                        var check = true;
                        foreach (var item2 in listEntity)
                        {
                            if (item.Id.Equals(item2.Id))
                            {
                                check = false;
                                break;
                            }
                        }
                        if (check)
                            listApps.Add(item);
                    }
                    if (listApps.Count > 0)
                        await _mediat.Send(new CreateSerialWareHouseCommand() { SerialWareHouseCommands = listApps });
                }
            }
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }

        private async Task<InwardDetailDTO> GetDataToDrop(InwardDetailDTO res, bool details = false)
        {
            var getUnit = new GetDropDownUnitCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, true)
            };
            var dataUnit = await _mediat.Send(getUnit);

            var getWareHouseItem = new GetDopDownWareHouseItemCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCacheName.WareHouseItemCacheNameDropDown, true)
            };
            var dataWareHouseItem = await _mediat.Send(getWareHouseItem);
            if (details)
            {
                res.WareHouseItemDTO = dataWareHouseItem.Where(x => x.Id == res.ItemId);
                res.UnitDTO = dataUnit.Where(x => x.Id == res.UnitId);
                res.GetAccountDTO = FakeData.GetListAccountIdentifier(_hostingEnvironment).Where(x => x.Id.Equals(res.AccountMore) || x.Id.Equals(res.AccountYes));
            }
            else
            {
                res.WareHouseItemDTO = dataWareHouseItem;
                res.UnitDTO = dataUnit;
                res.GetDepartmentDTO = FakeData.GetDepartment();
                res.GetCustomerDTO = FakeData.GetCustomer();
                res.GetEmployeeDTO = FakeData.GetEmployee();
                res.GetProjectDTO = FakeData.GetProject();
                res.GetStationDTO = FakeData.GetStation();
                res.GetAccountDTO = FakeData.GetListAccountIdentifier(_hostingEnvironment);

            }


            return res;
        }



        [Route("create-outward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateOut()
        {
            var res = new OutwardDetailDTO();
            await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }
        private async Task<OutwardDetailDTO> GetDataToDrop(OutwardDetailDTO res)
        {
            var getUnit = new GetDropDownUnitCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, true)
            };
            var dataUnit = await _mediat.Send(getUnit);

            var getWareHouseItem = new GetDopDownWareHouseItemCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCacheName.WareHouseItemCacheNameDropDown, true)
            };
            var dataWareHouseItem = await _mediat.Send(getWareHouseItem);

            res.WareHouseItemDTO = dataWareHouseItem;
            res.UnitDTO = dataUnit;
            res.GetDepartmentDTO = FakeData.GetDepartment();
            res.GetCustomerDTO = FakeData.GetCustomer();
            res.GetEmployeeDTO = FakeData.GetEmployee();
            res.GetProjectDTO = FakeData.GetProject();
            res.GetStationDTO = FakeData.GetStation();
            res.GetAccountDTO = FakeData.GetListAccountIdentifier(_hostingEnvironment);
            return res;
        }


        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(UnitCommands unitCommands)
        {
            var data = await _mediat.Send(new UpdateUnitCommand() { UnitCommands = unitCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(UnitCommands unitCommands)
        {
            var data = await _mediat.Send(new CreateUnitCommand() { UnitCommands = unitCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }

        [Route("create-inward-details")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(InwardDetailCommands inwardDetailCommands)
        {
            var data = await _mediat.Send(new CreateInwardDetailCommand() { InwardDetailCommands = inwardDetailCommands });
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
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }

        [Route("delete-details-inward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteInwarDetalis(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteInwardDetailCommand() { Id = listIds });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
    }
}