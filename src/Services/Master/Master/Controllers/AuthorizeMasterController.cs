﻿using Infrastructure;
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


        #region R
    //    [AllowAnonymous]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLisst([FromQuery] SearchModel search)
        {
            var list = await _userService.GetListUserAsync(search.Pages, search.Number, search.Id, search.Key);
            return Ok(new ResultMessageResponse()
            {
                success = false,
                data = list.Result,
                totalCount = list.totalCount
            });
        }






        #endregion


        [Route("role-edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult EditRole(string id)
        {

            var user = _userService.GetUserById(id);
            if (user == null)
            {
                var result = new ResultMessageResponse()
                {
                    success = false,
                    message="Tài khoản không tồn tại !"
                };
                return Ok(result);
            }
            var check = _userService.User;
            if(check.RoleNumber<user.RoleNumber)
            {
                var result = new ResultMessageResponse()
                {
                    success = false,
                    message = "Bạn không có quyền phân quyền tài khoản này !"
                };
                return Ok(result);
            }
            var res = new UserMasterModel()
            {
                Id = user.Id,
                Role = user.Role,
                Password = user.Password,
                Create = user.Create,
                Delete = user.Delete,
                Edit = user.Edit,
                InActive = user.InActive,
                Read = user.Read,
                RoleNumber = user.RoleNumber,
                UserName = user.UserName,
                WarehouseId = user.WarehouseId,
                ListWarehouseId = user.ListWarehouseId,
                RoleSelect= SelectListRole.Get()
            };

            return Ok(new ResultMessageResponse()
            {
                success = true,
                data=res
            });
        }


        [Route("role-edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditRoleAsync(UserMasterModel model)
        {

            var user = _userService.GetUserById(model.Id);
            if (user == null)
            {
                var re = new ResultMessageResponse()
                {
                    success = false,
                    message = "Tài khoản không tồn tại !"
                };
                return Ok(re);
            }
            var check = _userService.User;
            if (check.RoleNumber < model.RoleNumber)
            {
                var res1 = new ResultMessageResponse()
                {
                    success = false,
                    message = "Bạn không được phần quyền lớn hơn vai trò của bạn !"
                };
                return Ok(res1);
            }
            var res = new UserMaster()
            {
                Id = user.Id,
                Role = model.Role,
                Password = user.Password,
                Create = model.Create,
                Delete = model.Delete,
                Edit = model.Edit,
                InActive = model.InActive,
                Read = model.Read,
                RoleNumber = model.RoleNumber,
                UserName = user.UserName,
                WarehouseId = model.WarehouseId,
                ListWarehouseId = model.ListWarehouseId
            };
            var result =await _userService.SetRoleToUser(res);
            return Ok(new ResultMessageResponse()
            {
                success = result
            });
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
                Delete = model.Delete,
                Id = model.Id,
                InActive = model.InActive,
                Read = model.Read,
                RoleNumber = model.RoleNumber,
                Role = model.Role,
                WarehouseId = model.WarehouseId,
            };
            var user = _userService.GetUserById(model.Id);
            if (user != null)
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

        //   [CheckRole(LevelCheck.CREATE)]
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
                message = string.IsNullOrEmpty(res) ? "Tên tài khoản hoặc mật khẩu không chính xác !" : null
            };
            return Ok(result);
        }

    }
}

