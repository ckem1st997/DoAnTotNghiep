using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Validations.WareHouseLimit
{
    public partial class WareHouseLimitCommandValidator : AbstractValidator<WareHouseLimitCommands>
    {
        public WareHouseLimitCommandValidator()
        {
            RuleFor(order => order.ItemId).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Sản phẩm"));
            RuleFor(order => order.WareHouseId).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Kho"));
            RuleFor(order => order.MinQuantity).GreaterThanOrEqualTo(0)
                .WithMessage("Tồn tối thiểu lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.MaxQuantity).GreaterThanOrEqualTo(1).WithMessage("Tồn tối đa lớn hơn hoặc bằng 1 !");
        }
    }
}
