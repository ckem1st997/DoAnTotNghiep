using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers.BaseController
{
    [Authorize(Roles = "Admin,Manager")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerMaster : ControllerBase
    {
    }
}