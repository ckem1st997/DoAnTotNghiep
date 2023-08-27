using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WareHouse.API.Application.Authentication;

namespace WareHouse.API.Controllers.BaseController
{
   // [Authorize(Roles = "Admin,Manager,User")]
   // [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class BaseControllerWareHouse : ControllerBase
    {
        public BaseControllerWareHouse()
        {
        }    
    }
    
}