using GrpcGetDataToMaster;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share.BaseCore.BaseNop;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Controllers.BaseController;
using WareHouse.Domain.Entity;

namespace WareHouse.API.Controllers
{

    //[CheckRole(LevelCheck.READ)]

    public class GetDataGPRCController : BaseControllerWareHouse
    {
        private readonly IFakeData _ifakeData;
        private readonly GrpcGetData.GrpcGetDataClient _client;
        private readonly IRepositoryEF<Unit> _repositoryEF;
        private readonly IRepositoryEF<ListApp> _repositoryEFData;

        public GetDataGPRCController(GrpcGetData.GrpcGetDataClient client, IFakeData ifakeData)
        {
            _ifakeData = ifakeData;
            _client = client;
            _repositoryEF = EngineContext.Current.Resolve<IRepositoryEF<Unit>>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            _repositoryEFData = EngineContext.Current.Resolve<IRepositoryEF<ListApp>>(DataConnectionHelper.ConnectionStringNames.Master);

            //_repositoryEF = ExtensionFull.ResolveRepository<Unit>(DataConnectionHelper.ConnectionStringNames.Warehouse);
            //_repositoryEFData = ExtensionFull.ResolveRepository<ListApp>(DataConnectionHelper.ConnectionStringNames.Master);
        }


        [Route("get-list-account")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetListAccount()
        {
            var res = await _ifakeData.GetCreateBy();

            var result = new ResultMessageResponse()
            {
                data = res,
                success = true,
                totalCount = res.Count()
            };
            return Ok(result);
        }


        [Route("get-list-account1")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetListAccount1()
        {
            var res = await _repositoryEF.QueryAsync<UnitDTO>("Select * from Unit", commandType: System.Data.CommandType.Text);

            var result = new ResultMessageResponse()
            {
                data = res,
                success = true,
            };
            return Ok(result);
        }


        [Route("get-list-account11")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetListAccount11()
        {
            int ii = 0;
            // dùng ToListAsync
            // dapper: 1,2s
            // GetQueryable vs Table: 8.8s


            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                var h = _repositoryEFData.GetList(x => x.Id != null);
                ii++;
            }
            var res = _repositoryEFData.GetList(x => x.Id != null);
            var resc = watch.ElapsedMilliseconds;


            var watch1 = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                var h1 = _repositoryEFData.GetQueryable();
                ii++;
            }

            var res1 =  _repositoryEFData.GetQueryable();
            var resc1 = watch1.ElapsedMilliseconds;


            var watch2 = System.Diagnostics.Stopwatch.StartNew();


            for (int i = 0; i < 10000; i++)
            {
                var h2 = _repositoryEFData.Table;
                ii++;
            }
            var res2 =  _repositoryEFData.Table;
            var resc2 = watch2.ElapsedMilliseconds;



            var watch3 = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < 10000; i++)
            {
                var h3 =  _repositoryEFData.Query<ListApp>("Select * from ListApp", commandType: System.Data.CommandType.Text);
                ii++;
            }
            var res3 =  _repositoryEFData.Query<ListApp>("Select * from ListApp", commandType: System.Data.CommandType.Text);
            var resc3 = watch3.ElapsedMilliseconds;

            var result = new ResultMessageResponse()
            {
                data = new
                {
                    ii=ii,
                    resc = resc,
                    res1 = resc1,
                    res2 = resc2,
                    resc3 = resc3,
                    linq = res,
                    GetQueryable = res1,
                    Table = res2,
                    Query = res3
                },
                success = true,

            };
            return Ok(result);
        }


    }
}