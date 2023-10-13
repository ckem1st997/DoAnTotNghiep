using Dapper;
using GrpcGetDataToMaster;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share.Base.Core.Extensions;
using Share.Base.Core.Infrastructure;
using Share.Base.Service;
using Share.Base.Service.Controller;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Model;
using WareHouse.Domain.Entity;

namespace WareHouse.API.Controllers
{

    //[CheckRole(LevelCheck.READ)]

    public class GetDataGPRCController : BaseController
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

            var result = new MessageResponse()
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
            for (int i = 0; i < 1000000; i++)
            {
                var res1 = await _repositoryEFData.AddAsync(new ListApp()
                {
                    Id = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString(),
                });


            }
         var res=   await _repositoryEFData.SaveChangesConfigureAwaitAsync();

            var result = new MessageResponse()
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
            // nếu chạy đồng bộ thì


            //var watch = System.Diagnostics.Stopwatch.StartNew();

            //for (int i = 0; i < 10000; i++)
            //{
            //    var h = _repositoryEFData.GetList(x => x.Id != null && x.Id.Contains("15"));
            //    ii++;
            //}
            //var res = _repositoryEFData.GetList(x => x.Id != null && x.Id.Contains("15"));
            //var resc = watch.ElapsedMilliseconds;


            var watch1 = System.Diagnostics.Stopwatch.StartNew();

            //for (int i = 0; i < 10000; i++)
            //{
            //    var h1 = _repositoryEFData.GetQueryable();
            //    ii++;
            //}

            var res1 = _repositoryEFData.GetQueryable().Where(x => x.Id != null && x.Id.Contains("15"));
            var resc1 = watch1.ElapsedMilliseconds;


            var watch2 = System.Diagnostics.Stopwatch.StartNew();


            //for (int i = 0; i < 10000; i++)
            //{
            //    var h2 = _repositoryEFData.Table;
            //    ii++;
            //}
            var res2 = await _repositoryEFData.Table.Where(x => x.Id != null && x.Description.Contains("15")).ToListAsync();
            var resc2 = watch2.ElapsedMilliseconds;



            var watch3 = System.Diagnostics.Stopwatch.StartNew();

            //for (int i = 0; i < 3; i++)
            //{
            //    var regfdgfds3 = await _repositoryEFData.QueryAsync<ListApp>("Select * from ListApp where Description like '%15%'", commandType: System.Data.CommandType.Text);
            //    ii++;
            //}
            var res3 = await _repositoryEFData.QueryAsync<ListApp>("Select * from ListApp where Description like '%15%'", commandType: System.Data.CommandType.Text);
            var resc3 = watch3.ElapsedMilliseconds;



            var watch4 = System.Diagnostics.Stopwatch.StartNew();
            var pa = new DynamicParameters();
            pa.Add("@SearchKeyword", "15");
            //for (int i = 0; i < 3; i++)
            //{
            //    var resgfd4 = await _repositoryEFData.QueryAsync<ListApp>("usp_GetListAppByCriteria2", pa, commandType: System.Data.CommandType.StoredProcedure);
            //    ii++;
            //}

            var res4 = await _repositoryEFData.QueryAsync<ListApp>("usp_GetListAppByCriteria", pa, commandType: System.Data.CommandType.StoredProcedure);
            var resc4 = watch4.ElapsedMilliseconds;

            var result = new MessageResponse()
            {
                data = new
                {
                    ii = ii,
                    // resc = resc,
                    res1 = resc1,
                    res2 = resc2,
                    resc3 = resc3,
                    resc4 = resc4,
                    //  linq = res,
                    //  GetQueryable = res1,
                    //  Table = res2,
                    //  Query = res3
                },
                success = true,

            };
            return Ok(result);
        }


    }
}