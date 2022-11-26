

namespace Master.Controllers
{
    public class ListAuthozireRoleByUserController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;
        private readonly IUserService _userService;

        public ListAuthozireRoleByUserController(MasterdataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        [HttpGet]
        [Route("get-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList(string id)
        {
            var list = _context.ListAuthozireRoleByUsers.Where(x => x.Id != null && x.UserId.Equals(id)).Select(x=>x.ListAuthozireId);
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
                data = new ListAuthozireRoleByUser()
            });
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListAuthozireRoleByUser list)
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
            await _context.ListAuthozireRoleByUsers.AddAsync(list);
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveCacheListRole(list.UserId);
            return Ok(new ResultMessageResponse()
            {
                success = res
            });
        }


        [HttpPost]
        [Route("edit-update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(IEnumerable<string> ids, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            // check appId

            var listdelete = await _context.ListAuthozireRoleByUsers.AsNoTracking().Where(x => x.UserId.Equals(id)).ToListAsync();
            if (listdelete.Any())
                _context.ListAuthozireRoleByUsers.RemoveRange(listdelete);
            if (ids.Any())
                foreach (var item in ids)
                {
                    await _context.ListAuthozireRoleByUsers.AddAsync(new ListAuthozireRoleByUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = id,
                        ListAuthozireId = item
                    });
                }
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveCacheListRole(id);
            return Ok(new ResultMessageResponse()
            {
                success = res
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

         var res=   await _context.ListAuthozireRoleByUsers.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(new ResultMessageResponse()
            {
                success = res != null,
                data = res
            }); 
        }


        [HttpPost]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(ListAuthozireRoleByUser list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListAuthozireRoleByUsers.Update(list);
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveCacheListRole(list.UserId);
            return Ok(new ResultMessageResponse()
            {
                success = res
            });
        }



        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            bool res = false;
            var get = _context.ListAuthozireRoleByUsers.Where(x => listIds.Contains(x.Id));
            if (get != null)
            {
                _context.ListAuthozireRoleByUsers.RemoveRange(get);
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
