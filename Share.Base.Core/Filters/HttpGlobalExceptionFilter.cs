using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

using Share.Base.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Share.Base.Core.Filters
{
    /// custom response nếu có lỗi trong server ở phần base xảy ra ngoài ý muốn
    // bộ lọc Exceptions toàn server
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public HttpGlobalExceptionFilter()
        {
        }


        public void OnException(ExceptionContext context)
        {
            string getEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;
            var typeException = context.Exception.GetType();
            Log.Error(context.Exception + "|" + context.Exception.Message);
            Log.Error($"Exception Method: {context.Exception.TargetSite.Name}");
            Log.Error($"Type Exception: {context.Exception.GetType().FullName}");
            var jsonResult = new ResultMessageResponse();
            context.Result = new InternalServerErrorObjectResult(jsonResult);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (typeException == typeof(BaseException))
            {
                jsonResult = new ResultMessageResponse
                {
                    message = context.Exception.Message,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    success = false
                };              
            }
            if (typeException == typeof(RpcException))
            {
                jsonResult = new ResultMessageResponse
                {
                    message = "Hệ thống xác thực và phân quyền đang xảy ra lỗi, xin vui lòng thử lại sau !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                    success = false
                };
                context.Result = new UnauthorizedObjectResult(jsonResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                jsonResult = new ResultMessageResponse
                {
                    message = "Có lỗi ngoài ý muốn xảy ra, xin vui lòng liên hệ bộ phận liên quan !",
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    success = false
                };
                if (getEnv.Equals(Environments.Development))
                    jsonResult.message = context.Exception.Message;

            }

            context.ExceptionHandled = true;
        }
    }
}