using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Share.BaseCore.Extensions;
using Share.BaseCore.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Share.BaseCore.Authozire
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string _keyRole;

        // This constructor defines two required parameters: name and level.


        public AuthorizeRoleAttribute(string keyRole)
        {
            _keyRole = keyRole;

        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // get content param action
            //    public async Task<ActionResult<MemberDisplay?>> PostSave([ModelBinder(typeof(MemberBinder))] MemberSave contentItem)
            //var model = (MemberSave?)context.ActionArguments["contentItem"];

            //  IAuthozireExtensionForMaster iAuthenForMaster = context.HttpContext.RequestServices.GetService<IAuthozireExtensionForMaster>();

            bool checkRole = false, check401 = false;
            // create instance of IUserInfomationService not contractor
            IUserInfomationService _userService = EngineContext.Current.Resolve<IUserInfomationService>();
            // chưa xác thực
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                checkRole = await _userService.GetAuthozireByUserId("IsAuthenticatedFalse", _keyRole);
                check401 = true;
            }
            else
            {
                IAuthozireExtensionForMaster iAuthenForMaster = EngineContext.Current.Resolve<IAuthozireExtensionForMaster>();
                string userId = iAuthenForMaster.GetClaimType("id");
                // xác thực rồi
                if (_userService != null && !string.IsNullOrEmpty(userId))
                    checkRole = await _userService.GetAuthozireByUserId(userId, _keyRole);
            }

            if (!checkRole && _userService is null || !checkRole && check401)
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa xác thực người dùng !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                };
                context.Result = new UnauthorizedObjectResult(res);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (!checkRole)
            {
                var res = new ResultMessageResponse()
                {
                    data = null,
                    message = "Bạn chưa có quyền thực hiện thao tác này !",
                    httpStatusCode = (int)HttpStatusCode.Forbidden,
                };
                context.Result = new ObjectResult(res);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}