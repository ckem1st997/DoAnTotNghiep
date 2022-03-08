using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Validations
{
    public partial class OutwardCommandValidator : AbstractValidator<OutwardCommands>
    {
        public OutwardCommandValidator()
        {
            RuleFor(order => order.VoucherCode).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("số chứng từ thực tế"));
            RuleFor(order => order.WareHouseId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Kho"));
            RuleFor(order => order.VoucherDate).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("ngày tạo"));
            RuleFor(order => order.CreatedBy).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người tạo"));
            RuleFor(order => order.Deliver).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người giao"));
            RuleFor(order => order.Receiver).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người nhận"));
        }
    }
}