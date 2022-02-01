using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Validations
{
    public partial class VendorCommandValidator: AbstractValidator<VendorCommands>
    {
        public VendorCommandValidator()
        {
            RuleFor(order => order.Code).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Mã sản phẩm"));
            RuleFor(order => order.Code).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Mã sản phẩm"));
            RuleFor(order => order.Name).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
            RuleFor(order => order.Name).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
        }
    }
}