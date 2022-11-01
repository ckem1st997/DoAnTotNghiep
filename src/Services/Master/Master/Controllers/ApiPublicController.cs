using Core.Extensions;
using Infrastructure;
using Master.Application.Authentication;
using Master.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Master.Controllers
{
    [Authorize(Roles = "User,Admin,Manager")]
    [CheckRole(LevelCheck.READ)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ApiPublicController : Controller
    {
        private readonly IUserService _userService;
        private readonly MasterdataContext _context;

        public ApiPublicController(IUserService userService, MasterdataContext context)
        {
            _userService = userService;
            _context = context;
        }




        [Route("get-user")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetUser()
        {
            var user = _userService.User;
            return Ok(new ResultMessageResponse()
            {
                data = user,
                success = user != null
            });
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
            return Ok(new ResultMessageResponse()
            {
                data = list,
                success = list != null
            });
        }



        [Route("active-read")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ActiveUserRead(IEnumerable<string> listIds)
        {
            var user = _userService.User;
            var listHistory = _context.HistoryNotications.Where(_x => listIds.Contains(_x.Id));
            if (!listHistory.Any())
                return Ok(new ResultMessageResponse()
                {
                    success = false
                });
            foreach (var item in listHistory)
            {
                item.UserNameRead = item.UserNameRead + "," + user.UserName;
            }
            var res = await _context.SaveChangesAsync();
            return Ok(new ResultMessageResponse()
            {
                success = res == listHistory.Count()
            });
        }



    }
}
