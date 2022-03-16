using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Validations.BeginningWareHouse
{
 public partial class CheckUIQuantityCommandValidator: AbstractValidator<CheckUIQuantityCommands>
{
    public CheckUIQuantityCommandValidator()
    {
        RuleFor(order => order.ItemId).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Vật tư"));
        RuleFor(order => order.WareHouseId).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Kho"));      
    }
}
}