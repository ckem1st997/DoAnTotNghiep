using GrpcGetDataToMaster;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Controllers.BaseController;


namespace WareHouse.API.Controllers
{

    [CheckRole(LevelCheck.READ)]

    public class GetDataGPRCController : BaseControllerWareHouse
    {
        private readonly IFakeData _ifakeData;
        private readonly GrpcGetData.GrpcGetDataClient _client;

        public GetDataGPRCController(GrpcGetData.GrpcGetDataClient client,IFakeData ifakeData)
        {
            _ifakeData = ifakeData;
            _client = client;
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



    }
}