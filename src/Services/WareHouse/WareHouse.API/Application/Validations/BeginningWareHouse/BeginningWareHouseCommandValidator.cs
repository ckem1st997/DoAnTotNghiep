using FluentValidation;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Validations.BeginningWareHouse
{
    public partial class BeginningWareHouseCommandValidator: AbstractValidator<BeginningWareHouseCommands>
{
    public BeginningWareHouseCommandValidator()
    {
        RuleFor(order => order.ItemId).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Sản phẩm"));
        RuleFor(order => order.WareHouseId).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Kho"));
        RuleFor(order => order.Quantity).GreaterThanOrEqualTo(0).WithMessage("Số lượng phải lớn hơn hoặc bằng 0 !");
      
    }
}
}