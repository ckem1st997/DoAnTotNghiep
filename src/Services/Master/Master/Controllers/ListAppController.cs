using Core.Extensions;
using Infrastructure;
using Master.Application.Authentication;
using Master.Controllers.BaseController;
using Master.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Create()
        {
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data=new ListApp()
            });
        }


        [CheckRole(LevelCheck.CREATE)]
        [HttpPost]
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
            var res = await _context.ListApps.AddAsync(list);
            return Ok(new ResultMessageResponse()
            {
                success = res != null
            });
        }



        [HttpGet]
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

            var res = await _context.ListApps.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return Ok(new ResultMessageResponse()
            {
                success = true,
                data = res,
                message = res == null ? "Không tìm thấy !" : ""
            }); ;
        }


        [HttpPost]
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



    }
}
