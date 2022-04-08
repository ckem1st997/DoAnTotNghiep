using Infrastructure;
using Master.Application.Authentication;
using Master.Application.Message;
using Master.Controllers.BaseController;
using Master.Models;
using Master.Service;
using Microsoft.AspNetCore.Authorization;
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

        [Route("update")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register(UserMasterModel model)
        {
            var map = new UserMaster()
            {
                UserName = model.UserName,
                Create = model.Create,
                Edit = model.Edit,
                Delete = model.Delelte,
                Id = model.Id,
                InActive = model.InActive,
                Read = model.Read,
                RoleNumber = model.RoleNumber,
                Role = model.Role,
                WarehouseId = model.WarehouseId,
            };
            var user = _userService.GetUserById(model.Id);
            if (user!=null)
            {
                map.Password = user.Password;
                map.OnDelete = user.OnDelete;
                var res = await _userService.UpdateUser(map);
                var result = new ResultMessageResponse()
                {
                    success = res,
                };
                return Ok(result);
            }
            return Ok(new ResultMessageResponse()
            {
                success = false,
                message = "UserName không tồn tại !"
            });
        }

        [CheckRole(LevelCheck.CREATE)]
        [Route("get-user-login")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult GetUserLogin()
        {
            var res = _userService.User;

            if (res is null)
                return Unauthorized(new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa đặp nhập !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                });
            return Ok(new ResultMessageResponse()
            {
                data = _userService.User
            });
        }






        [AllowAnonymous]
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
            if (_userService.CheckUser(model.Username))
            {
                var res = await _userService.Register(map);
                var result = new ResultMessageResponse()
                {
                    success = res,
                };
                return Ok(result);
            }
            return Ok(new ResultMessageResponse()
            {
                success = false,
                message = "UserName đã tồn tại !"
            });
        }


        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Login(LoginViewModel model)
        {
            var map = new LoginModel()
            {
                Password = model.Password,
                Username = model.Username,
                Remember = model.Remember
            };
            var res = _userService.GenerateJWT(map);
            var result = new ResultMessageResponse()
            {
                data = res,
                success = !string.IsNullOrEmpty(res),
                message= string.IsNullOrEmpty(res)?"Tên tài khoản hoặc mật khẩu không chính xác !":null
            };
            return Ok(result);
        }

    }
}

