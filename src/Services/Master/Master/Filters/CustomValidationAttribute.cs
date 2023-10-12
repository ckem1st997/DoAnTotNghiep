using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Share.Base.Core.Extensions;

namespace Master.Filters
{
    /// <summary>
    /// custom reponse message to validator by flutent
    /// </summary>
    public class CustomValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();
                var responseObj = new MessageResponse
                {
                    code = "200",
                    message = "Đã xảy ra lỗi với dữ liệu đầu vào !",
                    errors = new Dictionary<string, IEnumerable<string>>()
                    {
                        {
                            "msg",
                            errors
                        }
                    },
                    success = false
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 200
                };
            }
        }
    }
}