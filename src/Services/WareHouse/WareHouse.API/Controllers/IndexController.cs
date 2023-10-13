using Microsoft.AspNetCore.Mvc;
using Share.Base.Core.Extensions;
using Share.Base.Service.Controller;
using Share.Base.Service.Security;
using System.Collections.Generic;
using System.Net;

namespace WareHouse.API.Controllers
{
    public class IndexController : ApiController
    {
        private readonly IAuthorizeExtension _authorizeExtension;
        public IndexController(IAuthorizeExtension authorizeExtension)
        {
            _authorizeExtension = authorizeExtension;
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
                message = "This is API to check Authorize !"
            });
        }

        [HttpGet]
        [Route("get-list-key")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList(bool getText)
        {
            var strings = GetKeyRoleHelper.GetKeyItems(getText);
            return Ok(new MessageResponse()
            {
                data = strings,
                totalCount = strings.Count
            });
        }
    }
}
