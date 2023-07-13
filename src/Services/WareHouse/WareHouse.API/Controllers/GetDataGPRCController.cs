using GrpcGetDataToMaster;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var res =await _ifakeData.GetCreateBy();

            var result = new ResultMessageResponse()
            {
                data = res,
                success = true,
                totalCount =res.Count() 
            };
            return Ok(result);
        }
          
        
        [Route("get-list-account1")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetListAccount1()
        {
            var res =await _repositoryEF.QueryAsync<UnitDTO>("Select * from Unit",commandType:System.Data.CommandType.Text);

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
            var res = _repositoryEFData.GetList(x => x.Id != null) ;

            var result = new ResultMessageResponse()
            {
                data = res.ToList(),
                success = true,
              
            };
            return Ok(result);
        }


    }
}