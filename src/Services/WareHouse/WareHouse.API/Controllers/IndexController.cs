using Microsoft.AspNetCore.Mvc;
using System.Net;
using WareHouse.API.Application.Message;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{
    public class IndexController : BaseControllerWareHouse
    {
        public IndexController()
        {

        }
        [Route("role")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Role()
        {
            return Ok(new ResultMessageResponse()
            {
                success = true,
                message = "This is API to check Authzire !"
            });
        }
    }
}
