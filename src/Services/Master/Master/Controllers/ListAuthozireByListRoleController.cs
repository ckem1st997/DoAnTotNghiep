

namespace Master.Controllers
{
    public class ListAuthozireByListRoleController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;
        private readonly IUserService _userService;


        public ListAuthozireByListRoleController(MasterdataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }


        [HttpGet]
        [Route("get-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList()
        {
            var list = _context.ListAuthozireByListRoles.Where(x => x.Id != null);
            return Ok(new ResultMessageResponse()
            {
                data = list,
                totalCount = list.Count()
            });
        }

        [HttpGet]
        [Route("get-list-id")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetListId(string id, string appId)
        {
            var list = _context.ListAuthozireByListRoles.Where(x => x.Id != null && x.AuthozireId.Equals(id) && x.AppId.Equals(appId)).Select(x => x.ListRoleId);
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
                data = new ListAuthozireByListRole()
            });
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListAuthozireByListRole list)
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
            await _context.ListAuthozireByListRoles.AddAsync(list);
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveAllCacheListRoleByUser();
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

            var res = await _context.ListAuthozireByListRoles.FirstOrDefaultAsync(x => x.Id.Equals(id));
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
        public async Task<IActionResult> Edit(ListAuthozireByListRole list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListAuthozireByListRoles.Update(list);
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveAllCacheListRoleByUser();
            return Ok(new ResultMessageResponse()
            {
                success = res
            });
        }

        [HttpPost]
        [Route("edit-update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(IEnumerable<string> ids, string id, string appId)
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

            var listdelete = await _context.ListAuthozireByListRoles.AsNoTracking().Where(x => x.AuthozireId.Equals(id) && x.AppId.Equals(appId)).ToListAsync();
            if (listdelete.Any())
                _context.ListAuthozireByListRoles.RemoveRange(listdelete);
            if (ids.Any())
                foreach (var item in ids)
                {
                    await _context.ListAuthozireByListRoles.AddAsync(new ListAuthozireByListRole()
                    {
                        Id = Guid.NewGuid().ToString(),
                        AuthozireId = id,
                        ListRoleId = item,
                        AppId = appId
                    });
                }
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveAllCacheListRoleByUser();
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
            var get = _context.ListAuthozireByListRoles.Where(x => listIds.Contains(x.Id));
            if (get != null)
            {
                _context.ListAuthozireByListRoles.RemoveRange(get);
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
