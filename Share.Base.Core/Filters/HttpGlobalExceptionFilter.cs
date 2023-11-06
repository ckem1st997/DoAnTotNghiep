using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
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
            Log.Error(context.Exception + "|" + context.Exception.Message + " | " + context.Exception.InnerException?.Message);
            Log.Error($"Exception Method: {context.Exception.TargetSite?.Name}");
            Log.Error($"Type Exception: {context.Exception.GetType().FullName}");
            var jsonResult = new MessageResponse();
            context.Result = new InternalServerErrorObjectResult(jsonResult);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (typeException == typeof(BaseException))
            {
                jsonResult = new MessageResponse
                {
                    message = context.Exception.Message,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    success = false
                };
                context.Result = new ObjectResult(jsonResult);

            }
            else if (typeException == typeof(UnauthorizedAccessException))
            {
                jsonResult = new MessageResponse
                {
                    message = "Bạn chưa xác thực, xin vui lòng đăng nhập lại !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                    success = false
                };
                context.Result = new UnauthorizedObjectResult(jsonResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (typeException == typeof(RpcException))
            {
                jsonResult = new MessageResponse
                {
                    message = "Hệ thống xác thực và phân quyền đang xảy ra lỗi, xin vui lòng thử lại sau !",
                    httpStatusCode = (int)HttpStatusCode.Unauthorized,
                    success = false
                };
                context.Result = new UnauthorizedObjectResult(jsonResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (typeException == typeof(DbUpdateException))
            {
                string errorMessage = context.Exception.InnerException?.Message ?? "";
                string dataMessage = "Có lỗi xảy ra khi cập nhật cơ sở dữ liệu. ";
                if (errorMessage.Contains("PRIMARY KEY constraint"))
                {
                    dataMessage += "Khóa chính hoặc dữ liệu đã tồn tại trong cơ sở dữ liệu !";
                }
                else if (errorMessage.Contains("REFERENCE constraint"))
                {
                    dataMessage += "Thao tác với dữ liệu chưa chính xác, xin vui lòng thử lại !";
                }
                else if (errorMessage.Contains("INSERT INTO"))
                {
                    dataMessage += "Lỗi khi thêm dữ liệu !";
                }
                else if (errorMessage.Contains("UPDATE"))
                {
                    dataMessage += "Lỗi khi cập nhật dữ liệu !";
                }
                else if (errorMessage.Contains("DELETE"))
                {
                    dataMessage += "Lỗi khi xóa dữ liệu !";
                }
                jsonResult = new MessageResponse
                {
                    message = dataMessage,
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    success = false
                };
                context.Result = new ObjectResult(jsonResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            else
            {
                jsonResult = new MessageResponse
                {
                    message = "Có lỗi ngoài ý muốn xảy ra, xin vui lòng liên hệ bộ phận liên quan !",
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    success = false
                };
                if (getEnv.Equals(Environments.Development))
                    jsonResult.message = context.Exception.Message;
                context.Result = new ObjectResult(jsonResult);

            }

            context.ExceptionHandled = true;
        }
    }
}