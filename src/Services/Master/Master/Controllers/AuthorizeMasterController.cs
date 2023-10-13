
using Share.Base.Core.Infrastructure;
using Share.Base.Service.Security;
using System.Security.Claims;

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


        //[CheckRole(LevelCheck.READ)]
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLisst([FromQuery] SearchModel search)
        {
            var list = await _userService.GetListUserAsync(search.Pages, search.Number, search.Id, search.Key);
            return Ok(new MessageResponse()
            {
                data = list.Result,
                totalCount = list.totalCount
            });
        }


        #endregion

        //[CheckRole(LevelCheck.UPDATE)]
        [Route("role-edit")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult EditRole(string id)
        {

            var user = _userService.GetUserById(id);
            if (user == null)
            {
                var result = new MessageResponse()
                {
                    success = false,
                    message = "Tài khoản không tồn tại !"
                };
                return Ok(result);
            }
            var check = _userService.User;
            if (check.RoleNumber < user.RoleNumber)
            {
                var result = new MessageResponse()
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
                RoleSelect = SelectListRole.Get()
            };

            return Ok(new MessageResponse()
            {
                success = true,
                data = res
            });
        }

        //[CheckRole(LevelCheck.UPDATE)]
        [Route("role-edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> EditRoleAsync(UserMasterModel model)
        {

            var user = _userService.CheckUser(model.UserName);
            if (user)
            {
                var re = new MessageResponse()
                {
                    success = false,
                    message = "Tài khoản không tồn tại !"
                };
                return Ok(re);
            }
            var check = _userService.User;
            if (check.RoleNumber < model.RoleNumber)
            {
                var res1 = new MessageResponse()
                {
                    success = false,
                    message = "Bạn không được phần quyền lớn hơn vai trò của bạn !"
                };
                return Ok(res1);
            }
            var res = new UserMaster()
            {
                Id = model.Id,
                Role = model.Role,
                Password = model.Password,
                Create = model.Create,
                Delete = model.Delete,
                Edit = model.Edit,
                InActive = model.InActive,
                Read = model.Read,
                RoleNumber = model.RoleNumber,
                UserName = model.UserName,
                WarehouseId = model.WarehouseId,
                ListWarehouseId = model.ListWarehouseId
            };
            var result = await _userService.SetRoleToUser(res);
            // var result = true;
            return Ok(new MessageResponse()
            {
                success = result
            });
        }


        //[CheckRole(LevelCheck.UPDATE)]
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
                var result = new MessageResponse()
                {
                    success = res,
                };
                return Ok(result);
            }
            return Ok(new MessageResponse()
            {
                success = false,
                message = "UserName không tồn tại !"
            });
        }

        //[CheckRole(LevelCheck.READ)]
        [Route("get-user-login")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public IActionResult GetUserLogin()
        {
            var res = _userService.User;

            if (res is null)
                return Unauthorized(new MessageResponse()
                {
                    data = null,
                    message = "Bạn chưa đặp nhập !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                });
            return Ok(new MessageResponse()
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
                var result = new MessageResponse()
                {
                    success = res,
                };
                return Ok(result);
            }
            return Ok(new MessageResponse()
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
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            var map = new LoginModel()
            {
                Password = model.Password,
                Username = model.Username,
                Remember = model.Remember
            };
            if (_userService.CheckUser(model.Username))
                return Ok(new MessageResponse()
                {
                    success = false,
                    message = "Tài khoản không tồn tại !"
                });
            if (!(await _userService.CheckActiveUser(model.Username)))
                return Ok(new MessageResponse()
                {
                    success = false,
                    message = "Tài khoản chưa được kích hoạt !"
                });
            if (!(await _userService.Login(map)))
                return Ok(new MessageResponse()
                {
                    success = false,
                    message = "Tài khoản hoặc mật khẩu chưa chính xác !"
                });
            IAuthorizeExtension iAuthenForMaster = EngineContext.Current.Resolve<IAuthorizeExtension>();
            string userId = _userService.GetUserByUserName(model.Username).Id;
            List<Claim> authClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, model.Username),
                                new Claim("id", userId),
                            };
            var res = iAuthenForMaster.GenerateJWT(authClaims, 7);
            if (!string.IsNullOrEmpty(res))
            {
                await _userService.RemoveCacheListRole(userId);
                await _userService.CacheListRole(userId);
            }
            var response = new ResponseLogin()
            {
                Jwt = res,
                User = _userService.GetUserByUserName(model.Username),
            };
            var result = new MessageResponse()
            {
                data = response,
                success = !string.IsNullOrEmpty(res),
                message = string.IsNullOrEmpty(res) ? "Tên tài khoản hoặc mật khẩu không chính xác !" : null
            };
            return Ok(result);
        }

    }
}

