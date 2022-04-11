using Master.Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers.BaseController
{
    [Authorize(Roles = "Admin,Manager")]
    [CheckRole(LevelCheck.READ)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerMaster : ControllerBase
    {
    }
}