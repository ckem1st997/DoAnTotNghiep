using Infrastructure;
using KafKa.Net;
using KafKa.Net.Abstractions;
using KafKa.Net.IntegrationEvents;
using Master.Application.Authentication;
using Master.Application.Message;
using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Master.Controllers
{
    [Authorize(Roles = "User,Admin,Manager")]
    [CheckRole(LevelCheck.CREATE)]
    [CheckRole(LevelCheck.WAREHOUSE)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class CallKafKaController : ControllerBase
    {
        private readonly ILogger<CallKafKaController> _logger;
        private readonly IKafKaConnection _kafKaConnection;
        private readonly IEventBus _eventBus;
        private readonly MasterdataContext _masterdataContext;
        private readonly IUserService _userService;

        public CallKafKaController(IUserService userServic,MasterdataContext masterdataContext,IEventBus eventBus, IKafKaConnection kafKaConnection, ILogger<CallKafKaController> logger)
        {
            _logger = logger;
            _kafKaConnection = kafKaConnection;
            _eventBus = eventBus;
            _masterdataContext = masterdataContext;
            _userService = userServic;
        }
        
        [Route("create-inward")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Test(InwardIntegrationEvent inward)
        {
            _logger.LogInformation("----- Sending integration event: {IntegrationEventId} at MasterAPI - ({@IntegrationEvent})", inward.Id, typeof(InwardIntegrationEvent));
            var user = _userService.User;
            if (user.RoleNumber != 3 && !user.WarehouseId.Contains(inward.WareHouseId))
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    message = "Bạn không có quyền thao tác với kho này !"
                });         
            _eventBus.Publish(inward);
            return Ok(new ResultMessageResponse()
            {
                success = true,
                message = "Thành công, xin vui lòng kiểm tra lại sau !"
            });
        }
    }
}
