using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Validations
{
    public partial class WareHouseCommandValidator : AbstractValidator<WareHouseCommands>
    {
        public WareHouseCommandValidator()
        {
            RuleFor(order => order.Code).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Mã sản phẩm"));
            RuleFor(order => order.Code).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Mã sản phẩm"));
            RuleFor(order => order.Name).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
            RuleFor(order => order.Name).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
        }
    }
}