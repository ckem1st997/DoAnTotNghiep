using MediatR;
using Microsoft.AspNetCore.Mvc;
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
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Querie.CheckCode;
using WareHouse.API.Application.Queries.DashBoard;
using WareHouse.API.Application.Queries.GetAll.WareHouses;
using WareHouse.API.Application.Queries.GetFisrt;
using WareHouse.API.Application.Queries.Report;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{

    [CheckRole(LevelCheck.READ)]
    public class DashBoardController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly ICacheExtension _cacheExtension;
        public DashBoardController(IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _cacheExtension = cacheExtension ?? throw new ArgumentNullException(nameof(cacheExtension));
        }
        #region R 


        [Route("get-select-top-inward-order-by")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] BaseDashboardCommands baseDashboardCommands)
        {
            var data = await _mediat.Send(new DashBoardSelectTopInwardCommand()
            {
                searchByDay = baseDashboardCommands.searchByDay,
                searchByMounth = baseDashboardCommands.searchByMounth,
                searchByYear = baseDashboardCommands.searchByYear,
                selectTopWareHouseBook = baseDashboardCommands.selectTopWareHouseBook,
                order = baseDashboardCommands.order
            });
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }

        [Route("get-select-chart-by-mouth")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SearchChartByMouth([FromQuery] DashBoardChartInAndOutCountByDayCommand dashBoardChartInAndOut)
        {
            var data = await _mediat.Send(dashBoardChartInAndOut);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null,
                totalCount = data.Inward.Sum(x => x.Count) + data.Outward.Sum(x => x.Count)
            };
            return Ok(result);
        }

        [Route("get-select-chart-by-year")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SearchChartByYear([FromQuery] DashBoardChartInAndOutCountByMouthCommand dashBoardChartInAndOut)
        {
            var data = await _mediat.Send(dashBoardChartInAndOut);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null,
                totalCount = data.Inward.Sum(x => x.Count) + data.Outward.Sum(x => x.Count)
            };
            return Ok(result);
        }


        [Route("get-select-chart-by-index")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SearchChartByIndex()
        {
            var data = await _mediat.Send(new SelectTopDashBoardCommand());
            var result = new ResultMessageResponse()
            {
                data = data,
                success = data != null,
                totalCount = 0
            };
            return Ok(result);
        }


        [Route("get-select-top-outward-order-by")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SelectTopOutward([FromQuery] BaseDashboardCommands baseDashboardCommands)
        {
            var data = await _mediat.Send(new DashBoardSelectTopOutwardCommand()
            {
                searchByDay = baseDashboardCommands.searchByDay,
                searchByMounth = baseDashboardCommands.searchByMounth,
                searchByYear = baseDashboardCommands.searchByYear,
                selectTopWareHouseBook = baseDashboardCommands.selectTopWareHouseBook,
                order = baseDashboardCommands.order
            });
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }


        [Route("get-select-top-out-in-order-by")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SelectTopOutAndIn([FromQuery] BaseDashboardCommands baseDashboardCommands)
        {
            var data = await _mediat.Send(new DashBoardSelectTopTotalOutAndInCommand()
            {
                searchByDay = baseDashboardCommands.searchByDay,
                searchByMounth = baseDashboardCommands.searchByMounth,
                searchByYear = baseDashboardCommands.searchByYear,
                selectTopWareHouseBook = baseDashboardCommands.selectTopWareHouseBook,
                order = baseDashboardCommands.order
            });
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }



        [Route("get-select-top-item-beginning-order-by")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DashBoardSelectTopItemBeginning([FromQuery] DashBoardSelectTopItemBeginningCommand orderBy)
        {
            var data = await _mediat.Send(orderBy);
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }


        [Route("get-select-top-warehouse-beginning-order-by")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DashBoardSelectTopWHBeginning([FromQuery] DashBoardSelectTopWareHouseBeginningCommand orderBy)
        {
            var data = await _mediat.Send(orderBy);
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }
        #endregion

        #region CUD

        #endregion
    }
}