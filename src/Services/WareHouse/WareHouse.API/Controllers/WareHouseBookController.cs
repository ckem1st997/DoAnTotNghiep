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
using WareHouse.API.Application.Querie.CheckCode;

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
            if (string.IsNullOrEmpty(IdItem))
            {
                var resError = new ResultMessageResponse()
                {
                    success = false,
                    message = "Chưa nhập Id của đơn vị !"
                };
                return Ok(resError);
            }
            var data = await _mediat.Send(new GetWareHouseUnitByIdItemCommand() { IdItem = IdItem });
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = 1
            };
            return Ok(result);
        }


        [Route("check-ui-quantity")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckUiQuantity([FromQuery] CheckUIQuantityCommands checkUIQuantityCommands)
        {
            var data = await _mediat.Send(new CheckUIQuantityCommand() { ItemId = checkUIQuantityCommands.ItemId, WareHouseId = checkUIQuantityCommands.WareHouseId });
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
            };
            return Ok(result);
        }

        #endregion

        #region CUD

        #region inward-details
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

        #endregion




        #region  outward-details

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
        private async Task<OutwardDetailDTO> GetDataToDrop(OutwardDetailDTO res, bool details = false)
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

        [Route("edit-outward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditOutwardDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }
            var res = await _mediat.Send(new OutwardDetailsGetFirstCommand() { Id = id });
            if (res != null)
                await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }



        [Route("details-outward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DetailsOutwardDetails(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var result1 = new ResultMessageResponse()
                {
                    message = "Id is null or empty"
                };
                return Ok(result1);
            }
            var res = await _mediat.Send(new OutwardDetailsGetFirstCommand() { Id = id });
            if (res != null)
                await GetDataToDrop(res, true);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }


        [Route("edit-outward-details")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditOutwardDetails(OutwardDetailCommands outwardDetailCommands)
        {
            if (outwardDetailCommands is null)
            {
                throw new ArgumentNullException(nameof(outwardDetailCommands));
            }

            var res = await _mediat.Send(new UpdateOutwardDetailCommand() { OutwardDetailCommands = outwardDetailCommands });
            if (res)
            {
                var listEntity = await _mediat.Send(new GetSerialByIdOutwardDetailsCommand() { Id = outwardDetailCommands.Id });
                if (listEntity.Count() == 0 && outwardDetailCommands.SerialWareHouses != null)
                {
                    await _mediat.Send(new CreateSerialWareHouseCommand() { SerialWareHouseCommands = outwardDetailCommands.SerialWareHouses });
                }
                else if (listEntity.Count() > 0 && outwardDetailCommands.SerialWareHouses == null)
                {
                    var listIds = new List<string>();
                    foreach (var item in outwardDetailCommands.SerialWareHouses)
                    {
                        listIds.Add(item.Id);
                        await _mediat.Send(new DeleteSerialWareHouseCommand() { Ids = listIds });

                    }

                }
                else if (listEntity.Count() > 0 && outwardDetailCommands.SerialWareHouses != null)
                {

                    var listIds = new List<string>();
                    foreach (var item in listEntity)
                    {
                        var check = true;
                        foreach (var item2 in outwardDetailCommands.SerialWareHouses)
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
                    foreach (var item in outwardDetailCommands.SerialWareHouses)
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
        [Route("create-outward-details")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(OutwardDetailCommands outwardDetailCommands)
        {
            var data = await _mediat.Send(new CreateOutwardDetailCommand() { OutwardDetailCommands = outwardDetailCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
        #endregion



        #region delete


        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(WareHouseBookDelete wareHouseBookDelete)
        {
            if (wareHouseBookDelete.idsIn is null && wareHouseBookDelete.idsOut is null)
            {
                var rese = new ResultMessageResponse()
                {
                    message = "Bạn chưa chọn phiếu nhập hoặc phiếu xuất nào !",
                    success = false
                };
                return Ok(rese);
            }
            var dataIn = true;
            var dataOut = true;
            var dataRes = true;
            var mes = "Xoá thành công !";
            if (wareHouseBookDelete.idsIn != null)
                dataIn = await _mediat.Send(new DeleteInwardCommand() { Id = wareHouseBookDelete.idsIn });
            if (wareHouseBookDelete.idsOut != null)
                dataOut = await _mediat.Send(new DeleteOutwardCommand() { Id = wareHouseBookDelete.idsOut });
            if (!dataIn && !dataOut)
            {
                dataRes = false;
                mes = "Có lỗi xảy ra, xin vui lòng thử lại !";
            }
            else if (dataIn && !dataOut)
            {
                mes = "Xoá thất bại phiếu xuất !";
            }
            else if (!dataIn && dataOut)
            {
                mes = "Xoá thất bại phiếu nhập !";
            }
            var result = new ResultMessageResponse()
            {
                success = dataRes,
                message = mes
            };
            return Ok(result);
        }

        [Route("delete-inward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteInward(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteOutwardCommand() { Id = listIds });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("delete-outward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOutward(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteInwardCommand() { Id = listIds });
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


        [Route("delete-details-outward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOutwarDetalis(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteOutwardDetailCommand() { Id = listIds });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
        #endregion



        #endregion



    }
}