using Microsoft.AspNetCore.Mvc;

namespace Master.Controllers
{
    public class ListRoleByUserController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;

        public ListRoleByUserController(MasterdataContext context)
        {
            _context = context;
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
                data = new ListRoleByUser()
            });
        }


        [CheckRole(LevelCheck.CREATE)]
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

           var res= await _context.ListRoleByUsers.FirstOrDefaultAsync(x => x.Id.Equals(id));
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