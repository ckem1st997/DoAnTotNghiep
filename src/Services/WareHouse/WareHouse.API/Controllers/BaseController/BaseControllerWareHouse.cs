using Microsoft.AspNetCore.Mvc;

namespace WareHouse.API.Controllers.BaseController
{
    //  [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
    }
}