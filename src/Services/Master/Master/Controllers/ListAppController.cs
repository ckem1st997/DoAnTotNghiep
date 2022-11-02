
using Infrastructure;
using Master.Application.Authentication;
using Master.Controllers.BaseController;
using Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Share.BaseCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Master.Controllers
{
    public class ListAppController : BaseControllerMaster
    {
        private readonly MasterdataContext _context;

        public ListAppController(MasterdataContext context)
        {
            _context = context;
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
                data = new ListApp()
            });
        }


        [CheckRole(LevelCheck.CREATE)]
        [HttpPost]
        [Route("create")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(ListApp list)
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
            await _context.ListApps.AddAsync(list);
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

            await _context.ListApps.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(new ResultMessageResponse()
            {
                success = await _context.SaveChangesAsync() > 0
            }); ;
        }


        [HttpPost]
        [Route("edit")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(ListApp list)
        {
            if (list is null)
            {
                return Ok(new ResultMessageResponse()
                {
                    success = true,
                    data = "Không nhận được dữ liệu"
                });
            }
            var res = await _context.ListApps.AddAsync(list);
            return Ok(new ResultMessageResponse()
            {
                success = res != null
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
            var get = _context.ListApps.Where(x => x.Id.Equals(listIds));
            if (get != null)
            {
                _context.ListApps.RemoveRange(get);
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
