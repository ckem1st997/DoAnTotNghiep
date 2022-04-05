using Master.Application.Message;
using Master.Extension;
using Master.Models;
using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net;

namespace Master.Application.Authentication
{
    //  [Authorize]
    //  [ActivatorUtilitiesConstructor]

    public class CheckRoleAttribute : ActionFilterAttribute
    {
        private IUserService _userService;
        public LevelCheck LevelCheck { get; set; }

        // This constructor defines two required parameters: name and level.


        public CheckRoleAttribute(LevelCheck level)
        {
            this.LevelCheck = level;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            // create instance of IUserService not contractor
            _userService = context.HttpContext.RequestServices.GetService<IUserService>();

            var checkRole = false;
            var user = _userService.User;

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

            if (checkRole == false)
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa có quyền !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                };
                context.Result = new UnauthorizedObjectResult(res);
            }
        }
    }
}