using Master.Application.Message;
using Master.Controllers.BaseController;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Master.Controllers
{
    public class IndexController : BaseControllerMaster
    {
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
