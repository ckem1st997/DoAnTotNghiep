using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers.BaseController
{
    //  [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerMaster : ControllerBase
    {
    }
}