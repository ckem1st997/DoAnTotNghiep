using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Share.BaseCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Share.BaseCore.Filters
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
            Log.Error(context.Exception + "|" + context.Exception.Message);
            if (context.Exception.GetType() == typeof(BaseException))
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details."
                };

                problemDetails.Errors.Add("DomainValidations", new string[] { context.Exception.Message });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
         
            else
            {
                var jsonResult = new ResultMessageResponse
                {
                    message = "Có lỗi ngoài ý muốn xảy ra, xin vui lòng liên hệ bộ phận liên quan !",
                    httpStatusCode = (int)HttpStatusCode.InternalServerError,
                    success = false
                };

                //if (env.IsDevelopment())
                //{
                //    jsonResult.data = ;
                //}
                // Result asigned to a result object but in destiny the response is empty. This is a known bug of .net core 1.1
                // It will be fixed in .net core 1.1.2. See https://github.com/aspnet/Mvc/issues/5594 for more information
                context.Result = new InternalServerErrorObjectResult(jsonResult);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}