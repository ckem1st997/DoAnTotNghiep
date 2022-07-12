using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.BaseModel;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt.WareHouses;
using WareHouse.API.Application.Queries.Paginated.WareHouses;
using WareHouse.API.Controllers.BaseController;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Model;
using WareHouse.Domain.IRepositories;
using Microsoft.AspNetCore.Authorization;
using WareHouse.Domain.Entity;
using Nest;
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;

namespace WareHouse.API.Controllers
{
    public class WareHousesController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        private readonly IRepositoryEF<Domain.Entity.WareHouse> _repository;
        private readonly IRepositoryEF<Domain.Entity.Outward> _repository1;
        private readonly IRepositoryEF<Domain.Entity.Inward> _repository2;
        private readonly IElasticClient _elasticClient;


        public WareHousesController(IMediator mediat, ICacheExtension cacheExtension, IRepositoryEF<Domain.Entity.WareHouse> repository, IRepositoryEF<Domain.Entity.Outward> repository1, IRepositoryEF<Inward> repository2, IElasticClient elasticClient)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
            _repository = repository;
            _repository1 = repository1;
            _repository2 = repository2;
            _elasticClient = elasticClient;
        }
        #region Raw

        [AllowAnonymous]
        [HttpGet("CountDataWareHouseBook")]
        public async Task<IActionResult> CountDataWareHouseBook()
        {
            var res = await _elasticClient.CountAsync<WareHouseBookDTO>();
            return Ok(new ResultMessageResponse()
            {
                data = res
            });
        }



        [AllowAnonymous]
        [HttpGet("GetDataWareHouseBook")]
        public async Task<IActionResult> GetDataWareHouseBook()
        {
            _elasticClient.DeleteByQuery<WareHouseBookDTO>(d => d.MatchAll());

            var res = await _mediat.Send(new WareHouseBookgetAllCommand());
            if (res.Result.Any())
                 await _elasticClient.IndexManyAsync(res.Result);
            return Ok(new ResultMessageResponse()
            {
                success = res.Result.Any()
            });


            //var person = new WareHouseDTO
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name= Guid.NewGuid().ToString()
            //};
            // var paginatedList = new PaginatedWareHouseBookCommand()
            //{
            //    Skip = 0,
            //    Take = 10
            //};
            //var res = await _mediat.Send(paginatedList);

            //foreach (var item in res.Result)
            //{
            //    _elasticClient.IndexDocument(item);
            //}
            //if(res !=null)
            //{
            //    var asyncIndexResponse = await _elasticClient.IndexDocumentAsync(res.Result);
            //    return Ok(asyncIndexResponse);

            //}
            //   var searchResponse = _elasticClient.Search<WareHouseBookDTO>(s => s.From(0).Size(10000).Fields("id"));


            //delete all
            //   var deleteByQueryResponse = _elasticClient.DeleteByQuery<WareHouseBookDTO>(d => d.MatchAll());
            // var deleteByQueryResponse = _elasticClient.DeleteByQuery<WareHouseBookDTO>(x=>x.Query(c=>c.Match(d=>d.Field(e=>e.Id== "1606d5a6-32e3-478a-8cd8-3573ee424df8"))));
            // var ids = new List<string> { "1606d5a6-32e3-478a-8cd8-3573ee424df8", "515e219a-a2fc-4202-8afa-cac7377e0231", "3" };
            // // done delete list id
            //// var bulkResponse = _elasticClient.DeleteMany<WareHouseBookDTO>(ids.Select(x => new WareHouseBookDTO { Id = x }));

            // // done by id
            // var bulone = _elasticClient.Delete<WareHouseBookDTO>(new WareHouseBookDTO() {Id= "524cd467-a17b-4054-9ea0-52c4a18b86fe" });
            //  return Ok(deleteByQueryResponse);
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
            var res = await _repository.UnitOfWork.SaveEntitiesAsync();
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
            await _repository1.UnitOfWork.SaveEntitiesAsync();


            return Ok(1);
        }


        #endregion





        #region R

        [CheckRole(LevelCheck.READ)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseCommand paginatedList)
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
        [Route("get-by-id")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] WareHouseGetFirstCommand firstCommand)
        {
            var data = await _mediat.Send(firstCommand);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true
            };
            return Ok(result);
        }


        [CheckRole(LevelCheck.READ)]
        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownWareHouseCommand paginatedList)
        {
            paginatedList.CacheKey = string.Format(WareHouseCacheName.WareHouseDropDown, paginatedList.Active);
            paginatedList.BypassCache = false;
            var data = await _mediat.Send(paginatedList);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.READ)]
        [Route("get-all")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var list = await _mediat.Send(new GetAllWareHouseCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, true)
            });
            var result = new ResultMessageResponse()
            {
                data = list
            };
            return Ok(result);
        }

        [CheckRole(LevelCheck.READ)]
        [Route("get-tree-view")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeViewAsync([FromQuery] GetTreeWareHouseCommand paginatedList)
        {
            var list = await _mediat.Send(new GetAllWareHouseCommand()
            {
                Active = paginatedList.Active,
                BypassCache = false,
                CacheKey = string.Format(WareHouseCacheName.WareHouseGetAll, paginatedList.Active)
            });
            paginatedList.WareHouseDTOs = list;
            var data = await _mediat.Send(paginatedList);
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
                    message = "Chưa nhập Id của kho !"
                };
                return Ok(resError);
            }
            var commandCheck = new WareHouseGetFirstCommand()
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
        public async Task<IActionResult> CreateAsync()
        {
            var mode = new WareHouseDTO()
            {
                Id = Guid.NewGuid().ToString(),
                Code = ExtensionFull.GetVoucherCode("WH")

            };
            await GetDataToDrop(mode);
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
                    message = "Chưa nhập Id của kho !"
                };
                return Ok(resError);
            }
            var commandCheck = new WareHouseGetFirstCommand()
            {
                Id = Id
            };
            var data = await _mediat.Send(commandCheck);

            await GetDataToDrop(data);
            var result = new ResultMessageResponse()
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
            var dataWareHouse = await _mediat.Send(getWareHouse);
            res.WareHouseDTOs = dataWareHouse;
            return res;
        }



        [CheckRole(LevelCheck.UPDATE)]
        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(WareHouseCommands wareHouseCommands)
        {
            if (wareHouseCommands is null)
                return Ok(new ResultMessageResponse()
                {
                    success = false,
                    message = "Không tồn tại !"
                });
            var commandCheck = new WareHouseGetFirstCommand()
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
            var res = await _mediat.Send(new UpdateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
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
        public async Task<IActionResult> Create(WareHouseCommands wareHouseCommands)
        {
            var check = await _mediat.Send(new WareHouseCodeCommand()
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
            var data = await _mediat.Send(new CreateWareHouseCommand() { WareHouseCommands = wareHouseCommands });
            if (data)
                await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
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
            var res = await _mediat.Send(new DeleteWareHouseCommand() { Id = listIds });
            if (res)
                await _cacheExtension.RemoveAllKeysBy(WareHouseCacheName.Prefix);
            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }
        #endregion
    }
}