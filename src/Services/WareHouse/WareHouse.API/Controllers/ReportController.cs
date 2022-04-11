using MediatR;
using Microsoft.AspNetCore.Mvc;
using Report.API.Application.Queries.GetAll.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WareHouse.API.Application.Authentication;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Queries.Report;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{
    [CheckRole(LevelCheck.READ)]
    public class ReportController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        public ReportController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
        }
        #region R     
        [Route("get-report-total")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] SearchReportTotalCommand searchReportTotalCommand)
        {
            var data = await _mediat.Send(searchReportTotalCommand);
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }

        [Route("get-report-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetReportDetals([FromQuery] SearchReportDetailsCommand searchReportTotalCommand)
        {
            var data = await _mediat.Send(searchReportTotalCommand);
            foreach (var item in data.Result)
            {
                item.Balance = item.Beginning + item.Import - item.Export;
            }
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }


        [Route("get-report-treeview")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetReportTreeView()
        {
            var data = await _mediat.Send(new ReportGetTreeViewCommand());
           
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }
        #endregion

        #region CUD

        #endregion
    }
}