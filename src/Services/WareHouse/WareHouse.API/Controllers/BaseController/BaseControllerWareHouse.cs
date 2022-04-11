using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WareHouse.API.Application.Authentication;

namespace WareHouse.API.Controllers.BaseController
{
    [Authorize(Roles = "User,Admin,Manager")]
    [CheckRole(LevelCheck.READ)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
    }
}