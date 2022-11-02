using Base.Events;
using KafKa.Net.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Share.BaseCore.Cache.CacheName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Controllers.BaseController;
using WareHouse.API.Infrastructure.ElasticSearch;

namespace WareHouse.API.Controllers
{
    public class OutwardController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        private readonly IFakeData _ifakeData;
        private readonly IUserSevice _userSevice;
        private readonly ILogger<OutwardController> _logger;
        private readonly IEventBus _eventBus;
        private readonly IElasticSearchClient<WareHouseBookDTO> _elasticSearchClient;

        public OutwardController(IEventBus eventBus, ILogger<OutwardController> logger, IUserSevice userSevice, IFakeData ifakeData, IMediator mediat, ICacheExtension cacheExtension, IElasticSearchClient<WareHouseBookDTO> elasticSearchClient)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
            _ifakeData = ifakeData ?? throw new ArgumentNullException(nameof(ifakeData));
            _userSevice = userSevice;
            _logger = logger;
            _eventBus = eventBus;
            _elasticSearchClient = elasticSearchClient;
        }
        #region R      
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
            var data = await _mediat.Send(new OutwardGetFirstCommand() { Id = id });
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.WareHouseId))
                {
                    var check = await _userSevice.CheckWareHouseIdByUser(data.WareHouseId);
                    if (!check)
                        return Unauthorized(new ResultMessageResponse()
                        {
                            success = false,
                            message = "Bạn không có quyền truy cập vào kho này !"
                        });
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
            var data = await _mediat.Send(new OutwardGetFirstCommand() { Id = id });
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.WareHouseId))
                {
                    var check = await _userSevice.CheckWareHouseIdByUser(data.WareHouseId);
                    if (!check)
                        return Unauthorized(new ResultMessageResponse()
                        {
                            success = false,
                            message = "Bạn không có quyền truy cập vào kho này !"
                        });
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
        public async Task<IActionResult> Edit(OutwardCommands OutwardCommands)
        {
            if (!string.IsNullOrEmpty(OutwardCommands.WareHouseId))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(OutwardCommands.WareHouseId);
                if (!check)
                    return Unauthorized(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    });
            }

            OutwardCommands.ModifiedDate = DateTime.Now;
            foreach (var item in OutwardCommands.OutwardDetails)
            {
                item.Amount = item.Uiquantity * item.Uiprice;
                int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = item.ItemId, IdUnit = item.UnitId });
                item.Quantity = (decimal)item.Uiquantity / convertRate;
                item.Price = item.Amount;
            }
            var data = await _mediat.Send(new UpdateOutwardCommand() { OutwardCommands = OutwardCommands });
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                //  mes = await _userSevice.CreateHistory(user.UserName, "Tạo", "vừa tạo mới phiếu nhập kho có mã " + inwardCommands.VoucherCode + "!", false, inwardCommands.Id);
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Chỉnh sửa",
                    Body = "vừa chỉnh sửa phiếu xuất kho có mã " + OutwardCommands.VoucherCode + "!",
                    Read = false,
                    Link = OutwardCommands.Id,
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    Log.Information($"----- Sending integration event: {kafkaModel.Id} at CreateHistoryIntegrationEvent - ({kafkaModel})");
                    if (_eventBus.IsConnectedProducer())
                        _eventBus.Publish(kafkaModel);
                    else
                        await _userSevice.CreateHistory(kafkaModel);
                    Log.Information($"----- Sending integration event: {kafkaModel.Id} at CreateHistoryIntegrationEvent - ({kafkaModel}) done...");

                }

                var resElastic = await _elasticSearchClient.InsertOrUpdateAsync(new WareHouseBookDTO()
                {
                    Id = OutwardCommands.Id,
                    CreatedBy = OutwardCommands.CreatedBy,
                    CreatedDate = OutwardCommands.CreatedDate,
                    Deliver = OutwardCommands.Deliver,
                    Description = OutwardCommands.Description,
                    ModifiedBy = OutwardCommands.ModifiedBy,
                    ModifiedDate = OutwardCommands.ModifiedDate,
                    Reason = OutwardCommands.Reason,
                    ReasonDescription = OutwardCommands.ReasonDescription,
                    Receiver = OutwardCommands.Receiver,
                    Type = "Phiếu xuất",
                    VoucherCode = OutwardCommands.VoucherCode,
                    VoucherDate = OutwardCommands.VoucherDate,
                    WareHouseId = OutwardCommands.WareHouseId,
                    WareHouseName = await _elasticSearchClient.GetNameWareHouse(OutwardCommands.WareHouseId)

                });

            }

            var result = new ResultMessageResponse()
            {
                success = data,
                data = data
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
                    return Unauthorized(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    });
            }
            var modelCreate = new OutwardDTO();
            modelCreate.Id = Guid.NewGuid().ToString();
            modelCreate.WareHouseId = idWareHouse;
            modelCreate.Voucher = ExtensionFull.GetVoucherCode("PX");
            await GetDataToDrop(modelCreate);
            var result = new ResultMessageResponse()
            {
                data = modelCreate,
                success = true
            };
            return Ok(result);
        }
        private async Task<OutwardDTO> GetDataToDrop(OutwardDTO res, bool details = false)
        {
            var getWareHouse = new GetDropDownWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, true)
            };
            var dataWareHouse = await _mediat.Send(getWareHouse);

            if (details)
            {
                res.WareHouseDTO = dataWareHouse.Where(x => x.Id.Equals(res.WareHouseId) || x.Id.Equals(res.ToWareHouseId));
                var createBy = await _ifakeData.GetCreateBy();
                res.GetCreateBy = createBy.Where(x => x.Id.Equals(res.CreatedBy));
            }
            else
            {
                res.WareHouseDTO = dataWareHouse;
                res.GetCreateBy = await _ifakeData.GetCreateBy();
            }

            return res;
        }



        [CheckRole(LevelCheck.CREATE)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(OutwardCommands OutwardCommands)
        {
            if (!string.IsNullOrEmpty(OutwardCommands.WareHouseId))
            {
                var check = await _userSevice.CheckWareHouseIdByUser(OutwardCommands.WareHouseId);
                if (!check)
                    return Unauthorized(new ResultMessageResponse()
                    {
                        success = false,
                        message = "Bạn không có quyền truy cập vào kho này !"
                    });
            }
            OutwardCommands.CreatedDate = DateTime.Now;
            OutwardCommands.ModifiedDate = DateTime.Now;
            foreach (var item in OutwardCommands.OutwardDetails)
            {
                item.Amount = item.Uiquantity * item.Uiprice;
                int convertRate = await _mediat.Send(new GetConvertRateByIdItemCommand() { IdItem = item.ItemId, IdUnit = item.UnitId });
                item.Quantity = (decimal)item.Uiquantity / convertRate;
                item.Price = item.Amount;
            }
            var data = await _mediat.Send(new CreateOutwardCommand() { OutwardCommands = OutwardCommands });
            if (data)
            {
                var user = await _userSevice.GetUser();
                // save history by Grpc
                //  mes = await _userSevice.CreateHistory(user.UserName, "Tạo", "vừa tạo mới phiếu nhập kho có mã " + inwardCommands.VoucherCode + "!", false, inwardCommands.Id);
                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Tạo",
                    Body = "vừa tạo mới phiếu xuất kho có mã " + OutwardCommands.VoucherCode + "!",
                    Read = false,
                    Link = OutwardCommands.Id,
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    Log.Information($"----- Sending integration event: {kafkaModel.Id} at CreateHistoryIntegrationEvent - ({kafkaModel})");
                    if (_eventBus.IsConnectedProducer())
                        _eventBus.Publish(kafkaModel);
                    else
                        await _userSevice.CreateHistory(kafkaModel);
                    Log.Information($"----- Sending integration event: {kafkaModel.Id} at CreateHistoryIntegrationEvent - ({kafkaModel}) done...");

                }
                var resElastic = await _elasticSearchClient.InsertOrUpdateAsync(new WareHouseBookDTO()
                {
                    Id = OutwardCommands.Id,
                    CreatedBy = OutwardCommands.CreatedBy,
                    CreatedDate = OutwardCommands.CreatedDate,
                    Deliver = OutwardCommands.Deliver,
                    Description = OutwardCommands.Description,
                    ModifiedBy = OutwardCommands.ModifiedBy,
                    ModifiedDate = OutwardCommands.ModifiedDate,
                    Reason = OutwardCommands.Reason,
                    ReasonDescription = OutwardCommands.ReasonDescription,
                    Receiver = OutwardCommands.Receiver,
                    Type = "Phiếu xuất",
                    VoucherCode = OutwardCommands.VoucherCode,
                    VoucherDate = OutwardCommands.VoucherDate,
                    WareHouseId = OutwardCommands.WareHouseId,
                    WareHouseName = await _elasticSearchClient.GetNameWareHouse(OutwardCommands.WareHouseId)
                });

            }

            var result = new ResultMessageResponse()
            {
                success = data,
                data = data
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
            var data = await _mediat.Send(new DeleteOutwardCommand() { Id = listIds });
            if (data)
            {

                var user = await _userSevice.GetUser();

                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Xóa",
                    Body = "vừa xóa phiếu nhập kho !",
                    Read = false,
                    Link = "",
                };
                using (LogContext.PushProperty("IntegrationEvent", $"{kafkaModel.Id}"))
                {
                    Log.Information($"----- Sending integration event: {kafkaModel.Id} at CreateHistoryIntegrationEvent - ({kafkaModel})");
                    if (_eventBus.IsConnectedProducer())
                        _eventBus.Publish(kafkaModel);
                    else
                        await _userSevice.CreateHistory(kafkaModel);
                    Log.Information($"----- Sending integration event: {kafkaModel.Id} at CreateHistoryIntegrationEvent - ({kafkaModel}) done...");

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
        #endregion
    }
}