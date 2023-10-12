using Share.Base.Core.Extensions;
using Infrastructure;
using Master.Controllers.BaseController;
using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Share.Base.Core.Infrastructure;

namespace Master.Controllers
{
    public class ListHistoryController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;
        private readonly IUserService _userService;
        public ListHistoryController(MasterdataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [Route("get-list-by-user")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetlistbyUser()
        {
            var user = _userService.User;
            var list = _context.HistoryNotications.Where(x => x.OnDelete == false);
            if (user.RoleNumber < 3)
                list = list.Where(x => x.UserName.Equals(user.UserName));
            list = list.OrderByDescending(x => x.CreateDate);
            return Ok(new MessageResponse()
            {
                data = list,
                success = list != null
            });
        }
    }
}
