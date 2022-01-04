using Microsoft.AspNetCore.Mvc;

namespace WareHouse.API.Controllers.BaseController
{
    // [Route($"api/{VesionApi.GetVisionToApi()}/[controller]")]
    // [Route("api/v1/[controller]")]
    //[ApiVersion("1.0")]
    //[ApiVersion("1.1")]
    //  [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
    }
}