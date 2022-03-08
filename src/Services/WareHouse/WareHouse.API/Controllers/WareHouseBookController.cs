using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.GetAll.Unit;
using WareHouse.API.Application.Queries.Paginated.Unit;
using WareHouse.API.Controllers.BaseController;
using WareHouse.API.Application.Cache.CacheName;
using WareHouse.API.Application.Queries.Paginated.WareHouseBook;
using WareHouse.API.Application.Model;
using WareHouse.API.Application.Queries.GetAll.WareHouseItem;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace WareHouse.API.Controllers
{
    public class WareHouseBookController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WareHouseBookController(IWebHostEnvironment hostEnvironment, IMediator mediat, ICacheExtension cacheExtension)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
            _hostingEnvironment = hostEnvironment ?? throw new ArgumentNullException(nameof(hostEnvironment));
        }
        #region R
        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedWareHouseBookCommand paginatedList)
        {
            var data = await _mediat.Send(paginatedList);
            var result = new ResultMessageResponse()
            {
                data = data.Result,
                success = true,
                totalCount = data.totalCount
            };
            return Ok(result);
        }

        [Route("get-drop-tree")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] GetDropDownUnitCommand command)
        {
            command.CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, command.Active);
            command.BypassCache = false;
            var data = await _mediat.Send(command);
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = data.Count()
            };
            return Ok(result);
        }


        [Route("get-unit-by-id")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUnitByIdAsync(string IdItem)
        {
            var data = await _mediat.Send(new GetWareHouseUnitByIdItemCommand() { IdItem = IdItem });
            var result = new ResultMessageResponse()
            {
                data = data,
                success = true,
                totalCount = 1
            };
            return Ok(result);
        }


        //public List<SelectListItem> GetListAccountIdentifier()
        //{
        //    var tmpPath = Path.Combine(_hostingEnvironment.WebRootPath, "/Excel/He_thong_tai_khoan kế toán.xlsx"); 
        //    Workbook wb = new Workbook(tmpPath);
        //    //Get the first worksheet.
        //    Worksheet worksheet = wb.Worksheets[0];
        //    //Get the cells collection.
        //    Cells cells = worksheet.Cells;

        //    //Define the list.
        //    var list = new List<SelectListItem>(); //Get the AA column index. (Since "Status" is always @ AA column.
        //    int col = CellsHelper.ColumnNameToIndex("A");
        //    //  int col2 = CellsHelper.ColumnNameToIndex("B");

        //    //Get the last row index in AA column.
        //    int last_row = worksheet.Cells.GetLastDataRow(col);

        //    //Loop through the "Status" column while start collecting values from row 9
        //    //to save each value to List
        //    for (int i = 2; i < 259; i++)
        //    {
        //        //    myList.Add(cells[i, col].Value.ToString(), cells[i, col + 1].Value.ToString());
        //        var code = cells[i, col].Value.ToString() == null ? "" : cells[i, col].Value.ToString();
        //        var name = cells[i, col + 1].Value.ToString() == null ? "" : cells[i, col + 1].Value.ToString();
        //        var tem = new SelectListItem();
        //        tem.Text = $"[{code.Trim()}] {name.Trim()}";
        //        tem.Value = code.Trim();
        //        list.Add(tem);
        //    }

        //    return list;
        //}




        #endregion

        #region CUD

        #endregion
        [Route("create-inward-details")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create()
        {
            var res = new InwardDetailDTO();
            await GetDataToDrop(res);
            var result = new ResultMessageResponse()
            {
                data = res
            };
            return Ok(result);
        }
        private async Task<InwardDetailDTO> GetDataToDrop(InwardDetailDTO res)
        {
            var getUnit = new GetDropDownUnitCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(UnitCacheName.UnitCacheNameDropDown, true)
            };
            var dataUnit = await _mediat.Send(getUnit);

            var getWareHouseItem = new GetDopDownWareHouseItemCommand()
            {
                Active = true,
                BypassCache = false,
                CacheKey = string.Format(WareHouseItemCacheName.WareHouseItemCacheNameDropDown, true)
            };
            var dataWareHouseItem = await _mediat.Send(getWareHouseItem);

            res.WareHouseItemDTO = dataWareHouseItem;
            res.UnitDTO = dataUnit;
            res.GetDepartmentDTO = FakeData.GetDepartment();
            res.GetCustomerDTO = FakeData.GetCustomer();
            res.GetEmployeeDTO = FakeData.GetEmployee();
            res.GetProjectDTO = FakeData.GetProject();
            res.GetStationDTO = FakeData.GetStation();
            return res;
        }

        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(UnitCommands unitCommands)
        {
            var data = await _mediat.Send(new UpdateUnitCommand() { UnitCommands = unitCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(UnitCommands unitCommands)
        {
            var data = await _mediat.Send(new CreateUnitCommand() { UnitCommands = unitCommands });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            var data = await _mediat.Send(new DeleteUnitCommand() { Id = listIds });
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }
    }
}