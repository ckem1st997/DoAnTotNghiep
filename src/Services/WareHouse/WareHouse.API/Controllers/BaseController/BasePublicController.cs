using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WareHouse.API.Controllers.BaseController
{
    [AllowAnonymous]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BasePublicControllerWareHouse : ControllerBase
    {
    }
}
