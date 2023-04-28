



using Serilog.Sinks.Http;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Master.Controllers
{

    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ApiPublicController : Controller
    {
        private readonly IUserService _userService;
        private readonly MasterdataContext _context;
        private readonly HttpClient _httpClient;

        public ApiPublicController(IUserService userService, MasterdataContext context, HttpClient httpClient)
        {
            _userService = userService;
            _context = context;
            _httpClient = httpClient;
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




        [Route("get-user-test")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetAsync()
        {

            // sẽ thực hiện delay 1000ms ngay khi gửi req tới http://google.com mà không cần đợi res từ http://google.com

            // Tạo đối tượng Stopwatch
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            var task1 = _httpClient.GetAsync("http://google.com");
            var res = task1.GetAwaiter();
            await Task.Delay(1000);

            Console.WriteLine("Kết quả: " + res.GetResult().IsSuccessStatusCode);
            // Dừng đo thời gian
            stopwatch1.Stop();
            // Lấy thời gian thực hiện của hành động
            TimeSpan elapsed1 = stopwatch1.Elapsed;

            // In ra thời gian thực hiện của hành động
            Console.WriteLine("Thời gian thực hiện 1: " + elapsed1.TotalMilliseconds + "ms");




            // sẽ đợi task trả về kết quả từ việc gọi đến http://google.com rồi đợi thêm 1000ms

            // Tạo đối tượng Stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var task = await _httpClient.GetAsync("http://google.com");

            await Task.Delay(1000);

            Console.WriteLine("Kết quả: " + task.IsSuccessStatusCode);
            // Dừng đo thời gian
            stopwatch.Stop();
            // Lấy thời gian thực hiện của hành động
            TimeSpan elapsed = stopwatch.Elapsed;

            // In ra thời gian thực hiện của hành động
            Console.WriteLine("Thời gian thực hiện : " + elapsed.TotalMilliseconds + "ms");

            ///

           






            return Ok(new ResultMessageResponse()
            {
                data = res,
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
