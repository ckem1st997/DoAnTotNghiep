using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;
using WareHouse.API.Application.Queries.Paginated.WareHouses;

using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Model;
using Microsoft.AspNetCore.Authorization;
using WareHouse.Domain.Entity;
using Nest;
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;
using WareHouse.API.Application.Interface;
using WareHouse.API.Infrastructure.ElasticSearch;
using Share.Base.Service.Caching.CacheName;
using Share.Base.Core.Infrastructure;
using Share.Base.Service;
using Share.Base.Service.Security;
using Share.Base.Core.Extensions;
using EasyCaching.Core;
using Share.Base.Service.Caching;
using Share.Base.Service.Controller;
using Share.Base.Service.Attribute;
using Share.Base.Service.Security.ListRole;
using Microsoft.Extensions.Logging;

namespace WareHouse.API.Controllers
{
    public class WareHousesController : ApiController
    {
        private readonly IHybridCachingManager _cacheExtension;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IRepositoryEF<Domain.Entity.Outward> _repository1;
        private readonly IRepositoryEF<Domain.Entity.Inward> _repository2;
        private readonly IElasticClient _elasticClient;
        private readonly IElasticSearchClient<WareHouseBookDTO> _elasticSearchClient;


        public WareHousesController(IHybridCachingManager cacheExtension, IElasticClient elasticClient, IElasticSearchClient<WareHouseBookDTO> elasticSearchClient)
        {
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
            _repository = EngineContext.Current.Resolve<IRepositoryEF<Domain.Entity.WareHouse>>(DataConnectionHelper.ConnectionStringNames.Warehouse); ;
            _repository1 = EngineContext.Current.Resolve<IRepositoryEF<Outward>>(DataConnectionHelper.ConnectionStringNames.Warehouse); ;
            _repository2 = EngineContext.Current.Resolve<IRepositoryEF<Inward>>(DataConnectionHelper.ConnectionStringNames.Warehouse); ;
            _elasticClient = elasticClient;
            _elasticSearchClient = elasticSearchClient;
        }
        #region Raw

        [AllowAnonymous]
        [HttpGet("FakeDataWareHouseBook")]
        public async Task<IActionResult> FakeDataWareHouseBook()
        {
            var res = await _mediator.Send(new WareHouseBookgetAllCommand());
            for (int i = 0; i < 5000; i++)
            {
                foreach (var item in res.Result)
                {
                    item.Id = Guid.NewGuid().ToString();
                    await _elasticClient.IndexDocumentAsync(item);
                }
            }

            return base.Ok(new MessageResponse()
            {
                success = res.Result.Any()
            });
        }


        [AllowAnonymous]
        [HttpGet("CountDataWareHouseBook")]
        public async Task<IActionResult> CountDataWareHouseBook()
        {
            var res = await _elasticClient.CountAsync<WareHouseBookDTO>();
            return base.Ok(new MessageResponse()
            {
                data = res
            });
        }

        [AllowAnonymous]
        [HttpGet("DeleteAll")]
        public async Task<IActionResult> DeleteAll()
        {
            var res = await _elasticClient.DeleteByQueryAsync<WareHouseBookDTO>(d => d.MatchAll());

            return base.Ok(new MessageResponse()
            {
                data = res
            });
        }


        [AllowAnonymous]
        [HttpPost("CreateDataWareHouseBook")]
        public async Task<IActionResult> CreateDataWareHouseBook([FromBody] WareHouseBookDTO ware)
        {
            ware.Id = Guid.NewGuid().ToString();
            var res = await _elasticSearchClient.InsertOrUpdateAsync(ware);
            return base.Ok(new MessageResponse()
            {
                data = await _elasticClient.SearchAsync<WareHouseBookDTO>(s => s.From(0).Size(150))
            });
        }


        [AllowAnonymous]
        [HttpPost("UpdateDataWareHouseBook")]
        public async Task<IActionResult> UpdateDataWareHouseBook([FromBody] WareHouseBookDTO ware)
        {
            var res = await _elasticSearchClient.InsertOrUpdateAsync(ware);
            return base.Ok(new MessageResponse()
            {
                data = await _elasticClient.SearchAsync<WareHouseBookDTO>(s => s.From(0).Size(150))
            });
        }

        [AllowAnonymous]
        [HttpPost("DeleteDataWareHouseBook")]
        public async Task<IActionResult> DeleteDataWareHouseBook(IEnumerable<string> listIds)
        {
            var res = await _elasticSearchClient.DeleteManyAsync(listIds);
            return base.Ok(new MessageResponse()
            {
                data = await _elasticClient.SearchAsync<WareHouseBookDTO>(s => s.From(0).Size(150))
            });
        }



        [AllowAnonymous]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList(string query)
        {
            //var request = new SearchRequest
            //{
            //    From = 0,
            //    Size = 10,
            //    Query = new TermQuery { Field = "user", Value = "kimchy" } ||
            //new MatchQuery { Field = "description", Query = "nest" }
            //};

            //var response = _elasticClient.Search<WareHouseBookDTO>(request);
            var list = new PaginatedListDynamic();
            var getlisst = await _elasticClient.SearchAsync<WareHouseBookDTO>(s => s.From(0).Size(15)
             .Query(q => q.Term(t => t.WareHouseName, query) || q.Match(mq => mq.Field(f => f.WareHouseName).Query(query))));
            list.Result = getlisst.Hits;
            var t = await _elasticClient.CountAsync<
                WareHouseBookDTO>(s => s.Query(q => q.Term(t => t.WareHouseName, query) || q.Match(mq => mq.Field(f => f.WareHouseName).Query(query))));
            list.totalCount = t.Count;
            return base.Ok(new MessageResponse()
            {
                data = list
            });
        }






        [AllowAnonymous]
        [HttpGet("CreateWareHouse")]
        public async Task<IActionResult> CreateWareHouse()
        {
            for (int i = 0; i < 1000000; i++)
            {
                await _repository.AddAsync(new Domain.Entity.WareHouse()
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = Guid.NewGuid().ToString(),
                    Address = Guid.NewGuid().ToString(),
                    Inactive = true,
                    Name = "Kho tạm " + i,
                });
            }
            var res = await _repository.SaveChangesConfigureAwaitAsync();
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("CreateIn")]
        public async Task<IActionResult> CreateIn()
        {
            var item = new string[3];
            item[0] = "15f148b2-f324-4376-8e40-a07315544dd5";
            item[1] = "ae973639-135a-48d7-8f04-0e5bfa660d1a";
            item[2] = "eda15932-8f25-47d1-b568-0641d078e06d";

            var itemUnit = new string[3];
            itemUnit[0] = "422aa3b8-93d8-4e5c-acaf-8c1a491e4d0a";
            itemUnit[1] = "0eb2a9a3-4b6b-4a7b-af94-e5804c5d7b8d";
            itemUnit[2] = "e71dce44-bdfd-4f17-ade3-aa5054c87be8";


            var itemwh = new string[3];
            itemwh[0] = "a3da7204-ec66-4f62-8c8a-72cda7740044";
            itemwh[1] = "84fd821c-2984-470b-8c1b-5123f7ddbd10";
            itemwh[2] = "fc8c5689-2d04-4dcf-9ed3-26f4c1522e0d";

            for (int i = 0; i < 500; i++)
            {
                await Task.Delay(1000);
                if (i % 2 == 0)
                {
                    var inwid = Guid.NewGuid().ToString();
                    var timenow = DateTime.Now;
                    var en = new Domain.Entity.Outward()
                    {
                        Id = inwid,
                        CreatedBy = "9a5fc419-63e0-423e-98f6-058c164c7a9b",
                        CreatedDate = timenow,
                        ModifiedDate = timenow,
                        VoucherCode = inwid,
                        VoucherDate = timenow,
                        Deliver = "Bên Test",
                        Receiver = "Bên Test 1",
                        Reason = "Nguyễn Văn B ",
                        Description = "Test",
                        DeliverPhone = "1234567890",
                        DeliverAddress = "Hà Nội",
                        DeliverDepartment = "CNTT",
                        ReceiverPhone = "1111111111",
                        ReceiverDepartment = "Hà Nội",
                    };
                    var rdj = new Random().Next(1, 3);
                    var id = Guid.NewGuid().ToString();
                    var itemm = new OutwardDetail()
                    {
                        Id = id,
                        OutwardId = en.Id,
                        ItemId = item[rdj],
                        UnitId = itemUnit[rdj],
                        Uiquantity = 5,
                        Uiprice = 70000,
                        Quantity = 15,
                        Amount = 35000,
                        Price = 35000

                    };
                    en.WareHouseId = itemwh[rdj];
                    en.OutwardDetails.Add(itemm);
                    await _repository1.AddAsync(en);
                    Console.WriteLine(i);
                }
                else
                {

                    var inwid = Guid.NewGuid().ToString();
                    var timenow = DateTime.Now;
                    var en = new Domain.Entity.Inward()
                    {
                        Id = inwid,
                        CreatedBy = "9a5fc419-63e0-423e-98f6-058c164c7a9b",
                        CreatedDate = timenow,
                        ModifiedDate = timenow,
                        VoucherCode = inwid,
                        VoucherDate = timenow,
                        Deliver = "Bên Test",
                        Receiver = "Bên Test 1",
                        VendorId = "68c36a4a-ee7b-4ac1-b5d5-c6f7cdb40250",
                        Reason = "Nguyễn Văn B ",
                        Description = "Test",
                        DeliverPhone = "1234567890",
                        DeliverAddress = "Hà Nội",
                        DeliverDepartment = "CNTT",
                        ReceiverPhone = "1111111111",
                        ReceiverDepartment = "Hà Nội",
                    };
                    var rdj = new Random().Next(1, 3);
                    var id = Guid.NewGuid().ToString();
                    var itemm = new InwardDetail()
                    {
                        Id = id,
                        InwardId = en.Id,
                        ItemId = item[rdj],
                        UnitId = itemUnit[rdj],
                        Uiquantity = 5,
                        Uiprice = 70000,
                        Quantity = 15,
                        Amount = 35000,
                        Price = 35000

                    };
                    en.WareHouseId = itemwh[rdj];
                    en.InwardDetails.Add(itemm);
                    await _repository2.AddAsync(en);
                    Console.WriteLine(i);
                }

            }
            await _repository1.SaveChangesConfigureAwaitAsync();


            return Ok(1);
        }


        #endregion





        #region R

        //  [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseReadKey.Warehouse)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseCommand paginatedList)
        {
            var data = await _mediator.Send(paginatedList);
            var result = new MessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }


        // [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseReadKey.Warehouse)]
        [Route("get-by-id")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] WareHouseGetFirstCommand firstCommand)
        {
            var data = await _mediator.Send(firstCommand);
            var result = new MessageResponse()
            {
                data = data,
                success = true
            };
            return Ok(result);
        }


        [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseReadKey.Warehouse)]
        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownWareHouseCommand paginatedList)
        {
            paginatedList.CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, paginatedList.Active);
            paginatedList.BypassCache = false;
            var data = await _mediator.Send(paginatedList);
            var result = new MessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }

        // [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseReadKey.Warehouse)]
        [Route("get-all")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            //for (int i = 0; i < 10000; i++)
            //{
            //    await _mediator.Send(new GetAllWareHouseCommand()
            //    {
            //        Active = true,
            //        BypassCache = false,
            //        CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, true)
            //    });
            //}
            Information("test log !");
            var list = await _mediator.Send(new GetAllWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, true)
            });
            var result = new MessageResponse()
            {
                data = list
            };
            return Ok(result);
        }

        [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseReadKey.Warehouse)]
        [Route("get-tree-view")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeViewAsync([FromQuery] GetTreeWareHouseCommand paginatedList)
        {
            var list = await _mediator.Send(new GetAllWareHouseCommand()
            {
                Active = paginatedList.Active,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, paginatedList.Active)
            });
            paginatedList.WareHouseDTOs = list;
            var data = await _mediator.Send(paginatedList);
            var result = new MessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }



        #endregion

        #region CUD

        // //[CheckRole(LevelCheck.READ)]
        [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseReadKey.Warehouse)]
        [Route("details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var resError = new MessageResponse()
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
            var resc = await _mediator.Send(commandCheck);
            if (resc is null)
                return base.Ok(new MessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });
            await GetDataToDrop(resc);
            var result = new MessageResponse()
            {
                data = resc,
                success = resc != null
            };
            return Ok(result);
        }


        //  [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseCreateKey.Warehouse)]
        [Route("create")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create()
        {
            var mode = new WareHouseCommands()
            {
                Code = Extension.GetVoucherCode("WH")
            };
            var result = new MessageResponse()
            {
                data = mode,
                success = mode != null
            };
            return Ok(result);
        }

        [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseEditKey.Warehouse)]
        [Route("edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditAsync(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                var resError = new MessageResponse()
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
            var data = await _mediator.Send(commandCheck);
            if (data != null)
                await GetDataToDrop(data);
            var result = new MessageResponse()
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
                CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, true)
            };
            var dataWareHouse = await _mediator.Send(getWareHouse);
            res.WareHouseDTOs = dataWareHouse;
            return res;
        }



        //[AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseEditKey.Warehouse)]
        //[Route("edit")]
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> Edit(WareHouseCommands wareHouseCommands)
        //{
        //    if (wareHouseCommands is null)
        //        return Ok(new MessageResponse()
        //        {
        //            success = false,
        //            message = "Không tồn tại !"
        //        });
        //    var commandCheck = new WareHouseGetFirstCommand()
        //    {
        //        Id = wareHouseCommands.Id
        //    };
        //    var resc = await _mediator.Send(commandCheck);
        //    if (resc is null)
        //        return Ok(new MessageResponse()
        //        {
        //            success = false,
        //            message = "Không tồn tại !"
        //        });
        //    var res = await _mediator.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
        //    if (res)
        //        await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
        //    var result = new MessageResponse()
        //    {
        //        success = res
        //    };
        //    return Ok(result);
        //}


        // [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseCreateKey.Warehouse)]
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(WareHouseCommands wareHouseCommands)
        {
            //var check = await _mediator.Send(new WareHouseCodeCommand()
            //{
            //    Code = wareHouseCommands.Code.Trim()
            //});
            //if (check)
            //{
            //    return Ok(new MessageResponse()
            //    {
            //        success = false,
            //        message = "Mã đã tồn tại, xin vui lòng chọn mã khác !"
            //    });
            //}
            //var data = await _mediator.Send(new CreateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
            //if (data)
            //    await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
            //var result = new MessageResponse()
            //{
            //    success = data
            //};
            return Ok(1);
        }

        [AuthorizeRole(AuthozireListKey.WarehouseKey.WarehouseDeleteKey.Warehouse)]
        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            var res = await _mediator.Send(new DeleteWareHouseCommand() { Id = listIds });
            if (res)
                await _cacheExtension.HybridCachingProvider.RemoveByPrefixAsync(WareHouseCacheName.Prefix);
            var result = new MessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion
    }
}
