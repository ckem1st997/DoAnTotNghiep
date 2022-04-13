

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;

namespace WareHouse.API.Application.Authentication
{
    [Authorize]
    //  [ActivatorUtilitiesConstructor]

    public class CheckRoleAttribute : ActionFilterAttribute
    {
        private IUserSevice _userService;
        public LevelCheck LevelCheck { get; set; }

        // This constructor defines two required parameters: name and level.


        public CheckRoleAttribute(LevelCheck level)
        {
            this.LevelCheck = level;

        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            // create instance of IUserService not contractor

            _userService = GetServiceByInterface<IUserSevice>.GetService();
            if (_userService == null)
                _userService = context.HttpContext.RequestServices.GetService<IUserSevice>();
            var checkRole = false;
            bool isRedirect = false;
            var mes403 = "Bạn chưa có quyền thực hiện thao tác này !";
            if (_userService != null)
            {
                var user = await _userService.GetUser();

                var create = user.Create;
                var update = user.Edit;
                var delete = user.Delete;
                var read = user.Read;
                var wh = user.WarehouseId;
                var roleNumber = user.RoleNumber;
                switch (LevelCheck)
                {
                    case LevelCheck.CREATE:
                        if (create)
                            checkRole = true;
                        break;
                    case LevelCheck.UPDATE:
                        if (update)
                            checkRole = true;
                        break;
                    case LevelCheck.DELETE:
                        if (delete)
                            checkRole = true;
                        break;
                    case LevelCheck.WAREHOUSE:
                        if (!string.IsNullOrEmpty(wh) && user.RoleNumber < 3 || roleNumber == 3)
                            checkRole = true;
                        mes403 = "Bạn không có quyền truy cập kho !";
                        isRedirect = true;
                        break;
                    default:
                        if (read)
                            checkRole = true;
                        break;
                }
            }


            if (_userService == null || checkRole == false && _userService.GetUser() == null)
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa xác thực người dùng !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                };
                var result = new UnauthorizedObjectResult(res);
                result.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = result;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (checkRole == false)
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = mes403,
                    httpStatusCode = (int)HttpStatusCode.Forbidden,
                    isRedirect = isRedirect
                };
                var result = new ObjectResult(res);
                context.Result = result;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            //     await next();
            await base.OnActionExecutionAsync(context, next);
        }
    }
}