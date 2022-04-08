using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WareHouse.API.Controllers.BaseController
{
    [Authorize(Roles = "User,Admin,Manager")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
    }
}