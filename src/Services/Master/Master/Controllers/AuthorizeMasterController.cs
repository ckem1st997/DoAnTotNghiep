using Master.Application.Message;
using Master.Controllers.BaseController;
using Master.Models;
using Master.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Master.Controllers
{
    public class AuthorizeMasterController : BaseControllerMaster
    {
        private readonly IUserService _userService;


        public AuthorizeMasterController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("register")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var map = new RegisterModel()
            {
                ConfirmPassword = model.ConfirmPassword,
                Password = model.Password,
                Username = model.Username,
            };
            var res = await _userService.Register(map);
            var result = new ResultMessageResponse()
            {
                success = res,
            };
            return Ok(result);
        }

        [Route("login")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var map = new LoginModel()
            {
                Password = model.Password,
                Username = model.Username,
                Remember=model.Remember            
            };
            var res = _userService.GenerateJWT(map);
            var result = new ResultMessageResponse()
            {
                data= res,
                success = !string.IsNullOrEmpty(res),
            };
            return Ok(result);
        }

    }
}

