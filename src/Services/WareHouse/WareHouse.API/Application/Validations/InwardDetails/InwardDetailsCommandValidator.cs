using FluentValidation;
using KafKa.Net.IntegrationEvents;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Validations
{
    public class InwardDetailsCommandValidator : AbstractValidator<InwardDetailCommands>
    {
        public InwardDetailsCommandValidator()
        {
            RuleFor(order => order.ItemId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Vật tư"));
            RuleFor(order => order.UnitId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Đơn vị tính"));
            RuleFor(order => order.Uiprice).GreaterThanOrEqualTo(0).WithMessage("Giá thành lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.Uiquantity).GreaterThanOrEqualTo(0).WithMessage("Số lượng lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.Amount).GreaterThanOrEqualTo(0).WithMessage("Thành tiền lớn hơn hoặc bằng 0 !");
        }
    } 
    
    public class InwardDetailIntegrationEventCommandValidator : AbstractValidator<InwardDetailIntegrationEvent>
    {
        public InwardDetailIntegrationEventCommandValidator()
        {
            RuleFor(order => order.ItemId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Vật tư"));
            RuleFor(order => order.UnitId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Đơn vị tính"));
            RuleFor(order => order.Uiprice).GreaterThanOrEqualTo(0).WithMessage("Giá thành lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.Uiquantity).GreaterThanOrEqualTo(0).WithMessage("Số lượng lớn hơn hoặc bằng 0 !");
            RuleFor(order => order.Amount).GreaterThanOrEqualTo(0).WithMessage("Thành tiền lớn hơn hoặc bằng 0 !");
        }
    }
}