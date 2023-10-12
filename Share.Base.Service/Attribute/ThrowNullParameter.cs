using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Attribute
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrowNullParameter : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ThrowNullParameter(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.ContainsKey(_parameterName))
            {
                context.Result = new BadRequestObjectResult($"Parameter {_parameterName} is missing.");
                return;
            }

            var parameterValue = context.ActionArguments[_parameterName];
            if (parameterValue == null)
            {
                context.Result = new BadRequestObjectResult($"Parameter {_parameterName} cannot be null.");
            }
        }
    }
}