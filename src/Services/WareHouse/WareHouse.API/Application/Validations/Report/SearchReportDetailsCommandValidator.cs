using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Queries.Report;

namespace WareHouse.API.Application.Validations
{
    public class SearchReportDetailsCommandValidator : AbstractValidator<SearchReportDetailsCommand>
    {
        public SearchReportDetailsCommandValidator()
        {
            RuleFor(order => order.WareHouseItemId).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Vật tư"));
            RuleFor(order => order.WareHouseId).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Kho"));
            RuleFor(order => order.ToDate).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Ngày kết thúc"));
            RuleFor(order => order.FromDate).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Ngày bắt đầu"));
        }
    }
}