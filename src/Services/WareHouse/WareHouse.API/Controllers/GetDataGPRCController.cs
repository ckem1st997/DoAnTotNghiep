using GrpcGetDataToMaster;
using Microsoft.AspNetCore.Mvc;
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

        public GetDataGPRCController(GrpcGetData.GrpcGetDataClient client, IFakeData ifakeData, IRepositoryEF<Unit> repositoryEF)
        {
            _ifakeData = ifakeData;
            _client = client;
            _repositoryEF = repositoryEF;
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
            var res =await _repositoryEF.QueryAsync<UnitDTO>("Select * from Unit");

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
            var res = _repositoryEF.QueryAsync<UnitDTO>("Select * from Unit");

            var result = new ResultMessageResponse()
            {
                data = res,
                success = true,
              
            };
            return Ok(result);
        }


    }
}