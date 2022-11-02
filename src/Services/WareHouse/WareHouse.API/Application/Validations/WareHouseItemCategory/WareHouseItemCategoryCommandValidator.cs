using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Validations
{
    public partial class WareHouseItemCategoryCommandValidator : AbstractValidator<WareHouseItemCategoryCommands>
    {
        public WareHouseItemCategoryCommandValidator()
        {
            RuleFor(order => order.Code).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Mã loại sản phẩm"));
            RuleFor(order => order.Code).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Mã loại sản phẩm"));
            RuleFor(order => order.Name).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Tên loại sản phẩm"));
            RuleFor(order => order.Name).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Tên loại sản phẩm"));
        }
    }
}