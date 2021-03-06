using Base.Events;
using KafKa.Net.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nest;
using Serilog;
using Serilog.Context;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;
using WareHouse.API.Controllers.BaseController;
using WareHouse.API.Infrastructure.ElasticSearch;
using WareHouse.Infrastructure;

namespace WareHouse.API.Controllers
{
    public class MasterGetController : BaseControllerWareHouse
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMediator _mediat;
        private readonly IUserSevice _userSevice;
        private readonly IEventBus _eventBus;
        private readonly IElasticSearchClient<WareHouseBookDTO> _elasticSearchClient;
        private readonly ICacheExtension _cacheExtension;
        private readonly WarehouseManagementContext _dbContext;


        public MasterGetController(IElasticClient elasticClient, IMediator mediat, IUserSevice userSevice, IElasticSearchClient<WareHouseBookDTO> elasticSearchClient, IEventBus eventBus, ICacheExtension cacheExtension, WarehouseManagementContext dbContext)
        {
            _elasticClient = elasticClient;
            _mediat = mediat;
            _userSevice = userSevice;
            _elasticSearchClient = elasticSearchClient;
            _eventBus = eventBus;
            _cacheExtension = cacheExtension;
            _dbContext = dbContext;
        }

        [HttpGet("GetDataWareHouseBook")]
        public async Task<IActionResult> GetDataWareHouseBook()
        {
            _elasticClient.DeleteByQuery<WareHouseBookDTO>(d => d.MatchAll());

            var res = await _mediat.Send(new WareHouseBookgetAllCommand());
            if (res.Result.Any())
            {
                var resdelete = await _elasticClient.IndexManyAsync(res.Result);
                var user = await _userSevice.GetUser();

                var kafkaModel = new CreateHistoryIntegrationEvent()
                {
                    UserName = user.UserName,
                    Method = "Chỉnh sửa",
                    Body = "vừa đồng bộ hóa giữa Sql và Elastic !",
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

            }
            return Ok(new ResultMessageResponse()
            {
                success = res.Result.Any()
            });
        }

        [HttpGet("GetIndexDataWareHouseBook")]
        public async Task<IActionResult> GetIndexDataWareHouseBook()
        {

            var res = await _mediat.Send(new WareHouseBookgetAllCommand());

            var resElastic = await _elasticSearchClient.CountAllAsync();
            return Ok(new ResultMessageResponse()
            {
                data = res.totalCount + " - " + resElastic,
                success = res.totalCount > 0 || resElastic > 0

            });

        }

        [HttpGet("DeleteAllElastic")]
        public async Task<IActionResult> DeleteAllElastic()
        {
  
            var res =await _elasticClient.DeleteByQueryAsync<WareHouseBookDTO>(d => d.MatchAll());
            if(res.IsValid)
            {
                var resCount = await _mediat.Send(new WareHouseBookgetAllCommand());
                return Ok(new ResultMessageResponse()
                {
                    success = resCount.totalCount>0

                });
            }
            return Ok(new ResultMessageResponse()
            {
                success = false

            });
        }

        [HttpGet("CanConnectSql")]
        public async Task<IActionResult> CanConnectSql()
        {
             var checkConnectoDb = await _dbContext.Database.CanConnectAsync();

            return Ok(new ResultMessageResponse()
            {
                success = checkConnectoDb

            });
        }


        [HttpGet("CanConnectRedis")]
        public IActionResult CanConnectRedis()
        {
            var checkConnectoDb = _cacheExtension.IsConnected;

            return Ok(new ResultMessageResponse()
            {
                success = checkConnectoDb

            });
        }     
        [HttpGet("CanConnectElastic")]
        public async Task <IActionResult> CanConnectElastic()
        {
            var checkConnectoDb = await _elasticClient.Cluster.HealthAsync();

            return Ok(new ResultMessageResponse()
            {
                success = checkConnectoDb.IsValid

            });
        }

        [HttpGet("DeleteAllCache")]
        public async Task<IActionResult> DeleteAllCache()
        {
            if (!_cacheExtension.IsConnected)
                return Ok(new ResultMessageResponse()
                {
                    data = "Không kết nối được tới Redis",
                    success = false

                });
            var res = await _cacheExtension.RemoveAll();
            return Ok(new ResultMessageResponse()
            {
                data = "Xóa thành công !",
                success = !res.IsNull

            });
        }
    }
}


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
