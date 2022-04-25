using FluentValidation;
using KafKa.Net.IntegrationEvents;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;

namespace WareHouse.API.Application.Validations.BeginningWareHouse
{
    public partial class InwardCommandValidator : AbstractValidator<InwardCommands>
    {
        public InwardCommandValidator()
        {
            RuleFor(order => order.Voucher).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("số chứng từ"));
            RuleFor(order => order.VoucherCode).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("số chứng từ thực tế"));
            RuleFor(order => order.WareHouseId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Kho"));
            RuleFor(order => order.VoucherDate).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("ngày tạo"));
            RuleFor(order => order.CreatedBy).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người tạo"));
            RuleFor(order => order.Deliver).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người giao"));
            RuleFor(order => order.Receiver).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người nhận"));           
            RuleFor(order => order.InwardDetails).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("chưa có vật tư"));           
        }
    }
    
    public partial class InwardIntegrationEventValidator : AbstractValidator<InwardIntegrationEvent>
    {
        public InwardIntegrationEventValidator()
        {
            RuleFor(order => order.Voucher).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("số chứng từ"));
            RuleFor(order => order.VoucherCode).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("số chứng từ thực tế"));
            RuleFor(order => order.WareHouseId).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Kho"));
            RuleFor(order => order.VoucherDate).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("ngày tạo"));
            RuleFor(order => order.CreatedBy).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người tạo"));
            RuleFor(order => order.Deliver).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người giao"));
            RuleFor(order => order.Receiver).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("người nhận"));           
            RuleFor(order => order.InwardDetails).NotNull().NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("chưa có vật tư"));           
        }
    }
}