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
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouseItem;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Aspose.Cells;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Querie.CheckCode;
using Microsoft.AspNetCore.Authorization;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Extensions;
using KafKa.Net.Abstractions;
using Microsoft.Extensions.Logging;
using Base.Events;
using Serilog.Context;
using WareHouse.API.Infrastructure.ElasticSearch;
using Share.BaseCore.Cache.CacheName;

namespace WareHouse.API.Controllers
{
    [CheckRole(LevelCheck.WAREHOUSE)]
    public class WareHouseBookController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly IFakeData _ifakeData;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IUserSevice _userSevice;
        private readonly IEventBus _eventBus;
        private readonly ILogger<WareHouseBookController> _logger;
        private readonly IElasticSearchClient<WareHouseBookDTO> _elasticSearchClient;

        public WareHouseBookController(IEventBus eventBus, ILogger<WareHouseBookController> logger, IUserSevice userSevice, IFakeData ifakeData, IWebHostEnvironment hostEnvironment, IMediator mediat, ICacheExtension cacheExtension, IElasticSearchClient<WareHouseBookDTO> elasticSearchClient)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _hostingEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
            _ifakeData = ifakeData ?? throw new ArgumentNullException(nameof(ifakeData));
            _userSevice = userSevice;
            _logger = logger;
            _eventBus = eventBus;
            _elasticSearchClient = elasticSearchClient;
        }
        #region R



        [CheckRole(LevelCheck.READ)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseBookCommand paginatedList)
        {
            if (!string.IsNullOrEmpty(paginatedList.WareHouseId))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(paginatedList.WareHouseId);
                if (!check)
                    return Ok(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    }); ;
            }

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

        [CheckRole(LevelCheck.READ)]

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

        [CheckRole(LevelCheck.READ)]

        [Route("check-ui-quantity")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CheckUiQuantity([FromQuery] CheckUIQuantityCommands checkUIQuantityCommands)
        {
            // trả về số theo đơn vị tính chính
            var data = await _mediat.Send(new CheckUIQuantityCommand() { ItemId = checkUIQuantityCommands.ItemId, WareHouseId = checkUIQuantityCommands.WareHouseId });
            int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = checkUIQuantityCommands.ItemId, IdUnit = checkUIQuantityCommands.UnitId });
            var result = new ResultMessageResponse()
            {
                // trả về số tồn theo đơn vị đang chọn
                data = data*convertRate,
                success = true,
            };
            return Ok(result);
        }

        #endregion

        #region CUD

        #region inward-details

        [CheckRole(LevelCheck.READ)]
        [Route("get-data-to-warehouse-book")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetData()
        {
            var res = new GetDataWareHouseBookBaseDTO();
            await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res,
                success = res != null
            };
            return Ok(result);
        }



        [CheckRole(LevelCheck.CREATE)]
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


        [CheckRole(LevelCheck.UPDATE)]
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



        [CheckRole(LevelCheck.READ)]
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


        [CheckRole(LevelCheck.UPDATE)]
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
            inwardDetailCommands.Amount = inwardDetailCommands.Uiquantity * inwardDetailCommands.Uiprice;
            int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = inwardDetailCommands.ItemId, IdUnit = inwardDetailCommands.UnitId });
            inwardDetailCommands.Quantity = (decimal)inwardDetailCommands.Uiquantity/ convertRate;
            inwardDetailCommands.Price = inwardDetailCommands.Amount;

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
            //if (res)
            //{
            //    var user = await _userSevice.GetUser();
            //    mes = await _userSevice.CreateHistory(user.UserName, "Chỉnh sửa", "vừa chỉnh sửa vật tư trong phiếu nhập kho" + data.VoucherCode + "!", false, data.Id);

            //}
            if (res)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var data = await _mediat.Send(new InwardGetFirstCommand() { Id = inwardDetailCommands.InwardId });
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Chỉnh sửa",
                    Body = "vừa chỉnh sửa vật tư trong phiếu nhập kho" + data.VoucherCode + "!",
                    Read = false,
                    Link = data.Id,
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }

            var result = new ResultMessageResponse()
            {
                success = res,
                data = res
            };
            return Ok(result);
        }

        private async Task<GetDataWareHouseBookBaseDTO> GetDataToDrop(GetDataWareHouseBookBaseDTO res, bool details = false)
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
            res.GetDepartmentDTO = await _ifakeData.GetDepartment();
            res.GetCustomerDTO = await _ifakeData.GetCustomer();
            res.GetEmployeeDTO = await _ifakeData.GetEmployee();
            res.GetProjectDTO = await _ifakeData.GetProject();
            res.GetStationDTO = await _ifakeData.GetStation();
            res.GetAccountDTO = _ifakeData.GetListAccountIdentifier(_hostingEnvironment);

            return res;
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
                res.GetAccountDTO = _ifakeData.GetListAccountIdentifier(_hostingEnvironment).Where(x => x.Id.Equals(res.AccountMore) || x.Id.Equals(res.AccountYes));
            }
            else
            {
                res.WareHouseItemDTO = dataWareHouseItem;
                res.UnitDTO = dataUnit;
                res.GetDepartmentDTO = await _ifakeData.GetDepartment();
                res.GetCustomerDTO = await _ifakeData.GetCustomer();
                res.GetEmployeeDTO = await _ifakeData.GetEmployee();
                res.GetProjectDTO = await _ifakeData.GetProject();
                res.GetStationDTO = await _ifakeData.GetStation();
                res.GetAccountDTO = _ifakeData.GetListAccountIdentifier(_hostingEnvironment);

            }


            return res;
        }


        [CheckRole(LevelCheck.CREATE)]
        [Route("create-inward-details")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(InwardDetailCommands inwardDetailCommands)
        {
            inwardDetailCommands.Amount = inwardDetailCommands.Uiquantity * inwardDetailCommands.Uiprice;
            int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = inwardDetailCommands.ItemId, IdUnit = inwardDetailCommands.UnitId });
            inwardDetailCommands.Quantity = (decimal)inwardDetailCommands.Uiquantity/convertRate;
            inwardDetailCommands.Price = inwardDetailCommands.Amount;
            var data = await _mediat.Send(new CreateInwardDetailCommand() { InwardDetailCommands = inwardDetailCommands });
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var res = await _mediat.Send(new InwardGetFirstCommand() { Id = inwardDetailCommands.InwardId });
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Tạo",
                    Body = "vừa tạo mới vật tư trong phiếu nhập kho" + res.VoucherCode + "!",
                    Read = false,
                    Link = res.Id,
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }
            var result = new ResultMessageResponse()
            {
                success = data,
                data = data
            };
            return Ok(result);
        }

        #endregion




        #region  outward-details
        [CheckRole(LevelCheck.CREATE)]
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
                res.GetAccountDTO = _ifakeData.GetListAccountIdentifier(_hostingEnvironment).Where(x => x.Id.Equals(res.AccountMore) || x.Id.Equals(res.AccountYes));
            }
            else
            {
                res.WareHouseItemDTO = dataWareHouseItem;
                res.UnitDTO = dataUnit;
                res.GetDepartmentDTO = await _ifakeData.GetDepartment();
                res.GetCustomerDTO = await _ifakeData.GetCustomer();
                res.GetEmployeeDTO = await _ifakeData.GetEmployee();
                res.GetProjectDTO = await _ifakeData.GetProject();
                res.GetStationDTO = await _ifakeData.GetStation();
                res.GetAccountDTO = _ifakeData.GetListAccountIdentifier(_hostingEnvironment);

            }

            return res;
        }


        [CheckRole(LevelCheck.UPDATE)]
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


        [CheckRole(LevelCheck.READ)]
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



        [CheckRole(LevelCheck.UPDATE)]
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
            outwardDetailCommands.Amount = outwardDetailCommands.Uiquantity * outwardDetailCommands.Uiprice;
            int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = outwardDetailCommands.ItemId, IdUnit = outwardDetailCommands.UnitId });
            outwardDetailCommands.Quantity = (decimal)outwardDetailCommands.Uiquantity/ convertRate ;
            outwardDetailCommands.Price = outwardDetailCommands.Amount;

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
            var mes = false;
            if (res)
            {
                var user = await _userSevice.GetUser();
                var data = await _mediat.Send(new OutwardGetFirstCommand() { Id = outwardDetailCommands.OutwardId });
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Chỉnh sửa",
                    Body = "vừa chỉnh sửa vật tư trong phiếu xuất kho" + data.VoucherCode + "!",
                    Read = false,
                    Link = data.Id
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    if (_eventBus.IsConnectedProducer())
                        _eventBus.Publish(kafkaModel);
                    else
                        await _userSevice.CreateHistory(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }

            var result = new ResultMessageResponse()
            {
                success = res,
                data = mes
            };
            return Ok(result);
        }



        [CheckRole(LevelCheck.CREATE)]
        [Route("create-outward-details")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(OutwardDetailCommands outwardDetailCommands)
        {
            outwardDetailCommands.Amount = outwardDetailCommands.Uiquantity * outwardDetailCommands.Uiprice;
            int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = outwardDetailCommands.ItemId, IdUnit = outwardDetailCommands.UnitId });
            outwardDetailCommands.Quantity = (decimal)outwardDetailCommands.Uiquantity / convertRate;
            outwardDetailCommands.Price = outwardDetailCommands.Amount;


            var data = await _mediat.Send(new CreateOutwardDetailCommand() { OutwardDetailCommands = outwardDetailCommands });
            var mes = false;
            if (data)
            {
                var user = await _userSevice.GetUser();
                var res = await _mediat.Send(new OutwardGetFirstCommand() { Id = outwardDetailCommands.OutwardId });
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Tạo",
                    Body = "vừa tạo mới vật tư trong phiếu xuất kho" + res.VoucherCode + "!",
                    Read = false,
                    Link =res.Id
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    if (_eventBus.IsConnectedProducer())
                        _eventBus.Publish(kafkaModel);
                    else
                        await _userSevice.CreateHistory(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }

            var result = new ResultMessageResponse()
            {
                success = data,
                data = mes
            };
            return Ok(result);
        }
        #endregion



        #region delete

        [CheckRole(LevelCheck.DELETE)]
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
            var listIdDelete = new List<string>();

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

                foreach (var item in wareHouseBookDelete.idsIn)
                {
                    var check = await _mediat.Send(new InwardGetFirstCommand() { Id = item });
                    if (check == null)
                        listIdDelete.Add(item);
                }
               
            }
            else if (!dataIn && dataOut)
            {
                mes = "Xoá thất bại phiếu nhập !";
                foreach (var item in wareHouseBookDelete.idsOut)
                {
                    var check = await _mediat.Send(new OutwardGetFirstCommand() { Id = item });
                    if (check == null)
                        listIdDelete.Add(item);
                }

            }
            else if (dataIn && dataOut)
            {
                foreach (var item in wareHouseBookDelete.idsOut)
                {
                    var check = await _mediat.Send(new OutwardGetFirstCommand() { Id = item });
                    if (check == null)
                        listIdDelete.Add(item);
                }
                foreach (var item in wareHouseBookDelete.idsIn)
                {
                    var check = await _mediat.Send(new InwardGetFirstCommand() { Id = item });
                    if (check == null)
                        listIdDelete.Add(item);
                }

            }
            if (listIdDelete.Count > 0)
                await _elasticSearchClient.DeleteManyAsync(listIdDelete);
            if (dataRes)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Xóa",
                    Body = "vừa xóa phiếu danh sách phiếu trong sổ kho !",
                    Read = false,
                    Link = "",
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }
            var result = new ResultMessageResponse()
            {
                success = dataRes,
                message = mes,
                data = dataRes

            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.DELETE)]
        [Route("delete-inward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteInward(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteInwardCommand() { Id = listIds });
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Xóa",
                    Body = "vừa xóa phiếu xuất trong sổ kho !",
                    Read = false,
                    Link = "",
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
                var listIdDelete = new List<string>();

                foreach (var item in listIds)
                {
                    var check = await _mediat.Send(new InwardGetFirstCommand() { Id = item });
                    if (check == null)
                        listIdDelete.Add(item);
                }
                if (listIdDelete.Count > 0)
                    await _elasticSearchClient.DeleteManyAsync(listIdDelete);
            }
            var result = new ResultMessageResponse()
            {
                success = data,
                data = data

            };
            return Ok(result);

        }

        [CheckRole(LevelCheck.DELETE)]
        [Route("delete-outward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOutward(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteOutwardCommand() { Id = listIds });
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Xóa",
                    Body = "vừa xóa phiếu xuất kho trong sổ kho !",
                    Read = false,
                    Link = "",
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
                var listIdDelete = new List<string>();

                foreach (var item in listIds)
                {
                    var check = await _mediat.Send(new OutwardGetFirstCommand() { Id = item });
                    if (check == null)
                        listIdDelete.Add(item);
                }
                if (listIdDelete.Count > 0)
                    await _elasticSearchClient.DeleteManyAsync(listIdDelete);
            }
            var result = new ResultMessageResponse()
            {
                success = data,
                data = data

            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.DELETE)]
        [Route("delete-details-inward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteInwarDetalis(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteInwardDetailCommand() { Id = listIds });
            //var mes = false;
            //if (data)
            //{
            //    var user = await _userSevice.GetUser();
            //    mes = await _userSevice.CreateHistory(user.UserName, "Xóa", "vừa xóa vật tư trong phiếu nhập kho !", false, "");

            //}
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Xóa",
                    Body = "vừa xóa vật tư trong phiếu nhập kho !",
                    Read = false,
                    Link = "",
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }
            var result = new ResultMessageResponse()
            {
                success = data,
                data = data

            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.DELETE)]
        [Route("delete-details-outward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteOutwarDetalis(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteOutwardDetailCommand() { Id = listIds });
            //var mes = false;
            //if (data)
            //{
            //    var user = await _userSevice.GetUser();
            //    mes = await _userSevice.CreateHistory(user.UserName, "Xóa", "vừa xóa vật tư trong phiếu xuất kho !", false, "");

            //}
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Xóa",
                    Body = "vừa xóa vật tư trong phiếu xuất kho  !",
                    Read = false,
                    Link = "",
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent})", kafkaModel.Id, kafkaModel);
                    _eventBus.Publish(kafkaModel);
                    _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at CreateHistoryIntegrationEvent - ({@IntegrationEvent}) done...", kafkaModel.Id, kafkaModel);

                }
            }
            var result = new ResultMessageResponse()
            {
                success = data,
                data = data

            };
            return Ok(result);

        }
        #endregion



        #endregion



    }
}