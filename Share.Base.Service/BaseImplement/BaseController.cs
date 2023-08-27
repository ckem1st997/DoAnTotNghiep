using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace Share.Base.Service.BaseImplement
{
    // [Authorize(Roles = "Admin,Manager,User")]
    // [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public BaseController()
        {
        }
    }

}