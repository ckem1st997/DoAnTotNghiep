﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            // create instance of IUserInfomationService not contractor
            IUserInfomationService _userService = EngineContext.Current.Resolve<IUserInfomationService>();
            IGetClaims iAuthenForMaster = EngineContext.Current.Resolve<IGetClaims>();
            bool checkRole = false;
            if (_userService != null)
                checkRole = await _userService.GetAuthozireByUserNameToKey(iAuthenForMaster.GetUserNameByClaims(), _keyRole);


            if (!checkRole && _userService == null)
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
                    message = "Bạn chưa có quyền thực hiện thao tác hoặc truy cập kho !",
                    httpStatusCode = (int)HttpStatusCode.Forbidden,
                };
                context.Result = new ObjectResult(res);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            await base.OnActionExecutionAsync(context, next);
        }
    }
}