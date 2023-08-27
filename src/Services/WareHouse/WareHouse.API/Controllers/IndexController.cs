using Microsoft.AspNetCore.Mvc;
using Share.Base.Core.Extensions;
using System.Net;
using WareHouse.API.Application.Message;

namespace WareHouse.API.Controllers
{
    public class IndexController : BaseController
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
