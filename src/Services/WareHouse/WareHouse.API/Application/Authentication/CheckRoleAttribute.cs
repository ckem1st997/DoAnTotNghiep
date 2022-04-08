using Master.Application.Authentication;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;
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
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            // create instance of IUserService not contractor
            _userService = context.HttpContext.RequestServices.GetService<IUserSevice>();
            var checkRole = false;
            if (_userService.GetUser() != null)
            {
                var user =await _userService.GetUser();

                var create = user.Create;
                var update = user.Edit;
                var delete = user.Delete;
                var read = user.Read;

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
                    default:
                        if (read)
                            checkRole = true;
                        break;
                }
            }


            if (checkRole == false && _userService.GetUser() == null)
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa xác thực người dùng !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                };
                context.Result = new UnauthorizedObjectResult(res);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                base.OnActionExecuting(context);
            }
            else
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa có quyền !",
                    httpStatusCode = (int)HttpStatusCode.Forbidden,
                };
                context.Result = new ObjectResult(res);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}