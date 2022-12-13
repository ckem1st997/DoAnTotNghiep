



namespace Master.Controllers
{

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
            ///AsEnumerable:nhanh hơn giảm từ 1.8, 1.7-1.5 s
            var user = _userService.User;
            var list = _context.HistoryNotications.Where(x => x.OnDelete == false);
            if (user.RoleNumber < 3)
                list = list.Where(x => x.UserName.Equals(user.UserName));
            list = list.OrderByDescending(x => x.CreateDate);
            return Ok(new ResultMessageResponse()
            {
                data = list.AsEnumerable(),
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
