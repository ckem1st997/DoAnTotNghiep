using FluentValidation;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Validations
{
    public class OutwardDetailsCommandValidator : AbstractValidator<OutwardDetailCommands>
    {
        public OutwardDetailsCommandValidator()
        {
            RuleFor(order => order.ItemId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Vật tư"));
            RuleFor(order => order.UnitId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Đơn vị tính"));
            RuleFor(order => order.Uiprice).GreaterThanOrEqualTo(0).WithMessage("Giá thành lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.Uiquantity).GreaterThanOrEqualTo(0).WithMessage("Số lượng lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.Amount).GreaterThanOrEqualTo(0).WithMessage("Thành tiền lớn hơn hoặc bằng 0 !");
        }
    }
}