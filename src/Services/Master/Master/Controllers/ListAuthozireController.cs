

namespace Master.Controllers
{
    public class ListAuthozireController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;

        public ListAuthozireController(MasterdataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList()
        {
            var list = _context.ListAuthozires.Where(x => x.Id != null);
            return Ok(new ResultMessageResponse()
            {
                data = list,
                totalCount = list.Count()
            });
        }



        [HttpGet]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create()
        {
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = new ListAuthozire()
            });
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListAuthozire list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            list.Id = Guid.NewGuid().ToString();
            await _context.ListAuthozires.AddAsync(list);
            return Ok(new ResultMessageResponse()
            {
                success = await _context.SaveChangesAsync() > 0
            });
        }



        [HttpGet]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    message = "Chưa nhập mã Id !"
                });
            }

          var res=  await _context.ListAuthozires.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(new ResultMessageResponse()
            {
                success = res != null,
                data = res
            }); ;
        }


        [HttpPost]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(ListAuthozire list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListAuthozires.Update(list);
            var res = await _context.SaveChangesAsync();
            return Ok(new ResultMessageResponse()
            {
                success = res > 0
            });
        }



        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            bool res = false;
            var get = _context.ListAuthozires.Where(x => listIds.Contains(x.Id));
            if (get != null)
            {
                _context.ListAuthozires.RemoveRange(get);
                res = await _context.SaveChangesAsync() > 0;
            }

            var result = new ResultMessageResponse()
            {
                success = res
            };
            return Ok(result);
        }

    }
}
