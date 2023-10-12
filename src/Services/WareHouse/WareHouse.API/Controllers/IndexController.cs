using Microsoft.AspNetCore.Mvc;
using Share.Base.Core.Extensions;
using System.Net;

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
            return base.Ok(new MessageResponse()
            {
                success = true,
                message = "This is API to check Authzire !"
            });
        }
    }
}
