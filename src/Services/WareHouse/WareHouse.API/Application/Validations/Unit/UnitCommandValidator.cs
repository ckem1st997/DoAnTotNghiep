using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Validations
{
    public partial class UnitCommandValidator : AbstractValidator<UnitCommands>
    {
        public UnitCommandValidator()
        {
            RuleFor(order => order.UnitName).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
            RuleFor(order => order.UnitName).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
        }
    }
}