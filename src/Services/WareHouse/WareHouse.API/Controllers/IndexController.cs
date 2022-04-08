using Microsoft.AspNetCore.Mvc;
using System.Net;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{
    public class IndexController : BaseControllerWareHouse
    {
        [Route("role")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Role()
        {
            return Ok();
        }
    }
}
