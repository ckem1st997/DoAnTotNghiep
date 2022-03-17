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
        public GetDataGPRCController()
        {

        }

        [Route("get-list-account")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetListAccount()
        {
            var result = new ResultMessageResponse()
            {
                data = FakeData.GetCreateBy(),
                success = true,
                totalCount = FakeData.GetCreateBy().Count()
            };
            return Ok(result);
        }



    }
}