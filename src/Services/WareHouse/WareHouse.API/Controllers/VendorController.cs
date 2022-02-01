using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WareHouse.API.Application.Commands.Create;
using WareHouse.API.Application.Commands.Delete;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Commands.Update;
using WareHouse.API.Application.Message;
using WareHouse.API.Application.Queries.Paginated.Vendor;
using WareHouse.API.Application.Validations;
using WareHouse.API.Controllers.BaseController;

namespace WareHouse.API.Controllers
{
    public class VendorController : BaseControllerWareHouse
    {
        private readonly IMediator _mediat;

        public VendorController(IMediator mediat)
        {
            _mediat = mediat ?? throw new ArgumentNullException(nameof(mediat));
        }

        [Route("get-list")]
        [HttpGet]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> IndexAsync([FromQuery] PaginatedVendorCommand paginatedList)
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


        [Route("edit")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Edit(VendorCommands vendorCommands)
        {
            // var validator = new VendorCommandValidator();
            // List<string> ValidationMessages = new List<string>();
            // var validationResult = validator.Validate(vendorCommands);
            var result = new ResultMessageResponse();
            // if (!validationResult.IsValid)
            // {
            //     result.success = false;
            //     foreach (ValidationFailure failure in validationResult.Errors)
            //     {
            //         ValidationMessages.Add(failure.ErrorMessage);
            //     }
            // }
            // result.errors.Add("error",ValidationMessages);
            result.success = await _mediat.Send(new UpdateVendorCommand() {VendorCommands = vendorCommands});

            return Ok(result);
        }


        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(VendorCommands vendorCommands)
        {
            var data = await _mediat.Send(new CreateVendorCommand() {VendorCommands = vendorCommands});
            var result = new ResultMessageResponse()
            {
                success = data
            };
            return Ok(result);
        }


        [Route("delete")]
        [HttpPost]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(IEnumerable<string> listIds)
        {
            var result = new ResultMessageResponse()
            {
                success = await _mediat.Send(new DeleteVendorCommand() {Id = listIds})
            };
            return Ok(result);
        }
    }
}