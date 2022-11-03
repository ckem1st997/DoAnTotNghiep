using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers
{
    public class ListRoleController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;

        public ListRoleController(MasterdataContext context)
        {
            _context = context;
        }



        [HttpGet]
        [Route("get-list")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult GetList()
        {
            var list = _context.ListRoles.Where(x => x.Id != null);
            return Ok(new ResultMessageResponse()
            {
                data = list,
                totalCount = list.Count()
            });
        }



        [CheckRole(LevelCheck.CREATE)]
        [HttpGet]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create()
        {
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = new ListRole()
            });
        }


        [CheckRole(LevelCheck.CREATE)]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListRole list)
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
            await _context.ListRoles.AddAsync(list);
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

          var res=  await _context.ListRoles.FirstOrDefaultAsync(x => x.Id.Equals(id));
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
        public async Task<IActionResult> Edit(ListRole list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            _context.ListRoles.Update(list);
            var res = await _context.SaveChangesAsync();
            return Ok(new ResultMessageResponse()
            {
                success = res > 0
            });
        }



        [CheckRole(LevelCheck.DELETE)]
        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            bool res = false;
            var get = _context.ListRoles.Where(x => listIds.Contains(x.Id));
            if (get != null)
            {
                _context.ListRoles.RemoveRange(get);
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