using Abp.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers
{
    public class ListRoleByUserController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;
        private readonly IUserService _userService;

        public ListRoleByUserController(MasterdataContext context, IUserService userService)
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
            var list = _context.ListRoleByUsers.Where(x => x.Id != null);
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
            var list = _context.ListRoleByUsers.Where(x => x.Id != null && x.UserId.Equals(id) && x.AppId.Equals(appId)).Select(x => x.ListRoleId);
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
                data = new ListRoleByUser()
            });
        }


        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListRoleByUser list)
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
            await _context.ListRoleByUsers.AddAsync(list);
            var res = await _context.SaveChangesAsync() > 0;
            if (res)
                await _userService.RemoveCacheListRole(list.UserId);
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

            var res = await _context.ListRoleByUsers.FirstOrDefaultAsync(x => x.Id.Equals(id));
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
        public async Task<IActionResult> Edit(ListRoleByUser list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListRoleByUsers.Update(list);
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
        public async Task<IActionResult> Edit(IEnumerable<string> ids, string id, string appId)
        {
            if (id.IsNullOrEmpty())
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            // check appId

            var listdelete = await _context.ListRoleByUsers.AsNoTracking().Where(x => x.UserId.Equals(id) && x.AppId.Equals(appId)).ToListAsync();
            if (listdelete.Any())
                _context.ListRoleByUsers.RemoveRange(listdelete);
            if (ids.Any())
                foreach (var item in ids)
                {
                    await _context.ListRoleByUsers.AddAsync(new ListRoleByUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = id,
                        ListRoleId = item,
                        AppId = appId
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




        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            bool res = false;
            var get = _context.ListRoleByUsers.Where(x => listIds.Contains(x.Id));
            if (get != null)
            {
                _context.ListRoleByUsers.RemoveRange(get);
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