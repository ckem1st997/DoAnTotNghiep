using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Share.Base.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Share.Base.Core.Filters
{
    /// <summary>
    /// custom reponse message to validator by flutent
    /// </summary>
    public class FilterValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage);
                var errorsString = errors.AnyList() ? string.Join("|", errors) : "Đã xảy ra lỗi với dữ liệu đầu vào !";
                var responseObj = new MessageResponse
                {
                    httpStatusCode = 500,
                    message = errorsString,
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
                    StatusCode = 500
                };
            }
        }
    }
}