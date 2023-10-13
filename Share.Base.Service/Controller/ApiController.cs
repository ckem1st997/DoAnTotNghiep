using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Share.Base.Core.Extensions;
using Share.Base.Core.Infrastructure;
using System.Reflection;
using System.Text;


namespace Share.Base.Service.Controller
{
    // [Authorize(Roles = "Admin,Manager,User")]
    // [Authorize]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private readonly IMediator Mediator;
        protected ApiController()
        {
            Mediator = CreateMediator();
        }

        public IMediator _mediator
        {
            get
            {
                return Mediator ?? CreateMediator();
            }
        }


        [NonAction]
        protected void Information(string message)
        {
            LogExtension.Information(message);
        }

        [NonAction]
        protected void Warning(string message)
        {
            LogExtension.Warning(message);
        }


        [NonAction]
        protected void Error(string message)
        {
            LogExtension.Error(message);
        }


        [NonAction]
        protected void Verbose(string message)
        {
            LogExtension.Verbose(message);
        }

        private IMediator CreateMediator()
        {
            return EngineContext.Current.Resolve<IMediator>();
        }
    }

}