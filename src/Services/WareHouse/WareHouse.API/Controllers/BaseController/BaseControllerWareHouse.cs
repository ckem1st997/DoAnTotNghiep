using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Controllers.BaseController
{
    [Authorize(Roles = "Admin,Manager")]
    [CheckRole(LevelCheck.READ)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
        public BaseControllerWareHouse()
        {
        }    
    }
    
}