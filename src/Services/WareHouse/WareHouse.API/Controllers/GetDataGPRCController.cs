using GrpcGetDataToMaster;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Controllers.BaseController;


namespace WareHouse.API.Controllers
{
    public class GetDataGPRCController : BaseControllerWareHouse
    {
        private readonly GrpcGetData.GrpcGetDataClient _client;

        public GetDataGPRCController(GrpcGetData.GrpcGetDataClient client)
        {
            _client = client;
        }





        [Route("get-list-account")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetListAccount()
        {
            var result = new ResultMessageResponse()
            {
                data = _client.GetCreateBy(new Params()),
                success = true,
                totalCount = FakeData.GetCreateBy().Count()
            };
            return Ok(result);
        }



    }
}