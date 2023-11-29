

namespace Master.Controllers
{
    public class ListAppController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;

        public ListAppController(MasterdataContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("get-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetList()
        {
            var list = await _context.ListApps.Where(x => x.Id != null).ToListAsync();
            return Ok(new MessageResponse()
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
            return Ok(new MessageResponse()
            {
                success = true,
                data = new ListApp()
            });
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListApp list)
        {
            if (list is null)
            {
                return Ok(new MessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            list.Id = Guid.NewGuid().ToString();
            await _context.ListApps.AddAsync(list);
            return Ok(new MessageResponse()
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
                return Ok(new MessageResponse()
                {
                    success = true,
                    message = "Chưa nhập mã Id !"
                });
            }

            var res = await _context.ListApps.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(new MessageResponse()
            {
                success = res != null,
                data = res
            });
        }


        [HttpPost]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(ListApp list)
        {
            if (list is null)
            {
                return Ok(new MessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListApps.Update(list);
            var res = await _context.SaveChangesAsync();
            return Ok(new MessageResponse()
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
            var get = _context.ListApps.Where(x => listIds.Contains(x.Id));
            if (get != null && get.Count() > 0)
            {
                _context.ListApps.RemoveRange(get);
                res = await _context.SaveChangesAsync() > 0;
            }

            var result = new MessageResponse()
            {
                success = res
            };
            return Ok(result);
        }

    }
}
