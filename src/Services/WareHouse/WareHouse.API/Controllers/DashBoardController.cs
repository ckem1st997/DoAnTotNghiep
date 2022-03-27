using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        public async Task<IActionResult> IndexAsync([FromQuery] DashBoardSelectTopInwardCommand dashBoardSelectTopInward)
        {
            var data = await _mediat.Send(dashBoardSelectTopInward);        
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }


        [Route("get-select-top-outward-order-by")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SelectTopOutward([FromQuery] DashBoardSelectTopOutwardCommand dashBoardSelectTopOutward)
        {
            var data = await _mediat.Send(dashBoardSelectTopOutward);
            if (dashBoardSelectTopOutward.selectTopWareHouseBook == SelectTopWareHouseBook.Count)
            {
                if (dashBoardSelectTopOutward.order == "asc")
                {
                    data.Result = data.Result.OrderBy(x => x.Count);
                }
                else if (dashBoardSelectTopOutward.order == "desc")
                {
                    data.Result = data.Result.OrderByDescending(x => x.Count);
                }
            }

            else if (dashBoardSelectTopOutward.selectTopWareHouseBook == SelectTopWareHouseBook.SumPrice)
            {
                if (dashBoardSelectTopOutward.order == "asc")
                {
                    data.Result = data.Result.OrderBy(x => x.SumPrice);
                }
                else if (dashBoardSelectTopOutward.order == "desc")
                {
                    data.Result = data.Result.OrderByDescending(x => x.SumPrice);
                }
            }
            else if (dashBoardSelectTopOutward.selectTopWareHouseBook == SelectTopWareHouseBook.SumQuantity)
            {
                if (dashBoardSelectTopOutward.order == "asc")
                {
                    data.Result = data.Result.OrderBy(x => x.SumQuantity);
                }
                else if (dashBoardSelectTopOutward.order == "desc")
                {
                    data.Result = data.Result.OrderByDescending(x => x.SumQuantity);
                }
            }
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
        public async Task<IActionResult> SelectTopOutAndIn([FromQuery] DashBoardSelectTopTotalOutAndInCommand dashBoardSelectTopTotalOutAndIn)
        {
            var data = await _mediat.Send(dashBoardSelectTopTotalOutAndIn);
            if (dashBoardSelectTopTotalOutAndIn.selectTopWareHouseBook == SelectTopWareHouseBook.Count)
            {
                if (dashBoardSelectTopTotalOutAndIn.order == "asc")
                {
                    data.Result = data.Result.OrderBy(x => x.Count);
                }
                else if (dashBoardSelectTopTotalOutAndIn.order == "desc")
                {
                    data.Result = data.Result.OrderByDescending(x => x.Count);
                }
            }

            else if (dashBoardSelectTopTotalOutAndIn.selectTopWareHouseBook == SelectTopWareHouseBook.SumPrice)
            {
                if (dashBoardSelectTopTotalOutAndIn.order == "asc")
                {
                    data.Result = data.Result.OrderBy(x => x.SumPrice);
                }
                else if (dashBoardSelectTopTotalOutAndIn.order == "desc")
                {
                    data.Result = data.Result.OrderByDescending(x => x.SumPrice);
                }
            }
            else if (dashBoardSelectTopTotalOutAndIn.selectTopWareHouseBook == SelectTopWareHouseBook.SumQuantity)
            {
                if (dashBoardSelectTopTotalOutAndIn.order == "asc")
                {
                    data.Result = data.Result.OrderBy(x => x.SumQuantity);
                }
                else if (dashBoardSelectTopTotalOutAndIn.order == "desc")
                {
                    data.Result = data.Result.OrderByDescending(x => x.SumQuantity);
                }
            }
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