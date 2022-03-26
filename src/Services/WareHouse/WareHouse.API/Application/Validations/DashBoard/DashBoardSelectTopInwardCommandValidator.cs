using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Queries.DashBoard;
using WareHouse.API.Application.Queries.Report;

namespace WareHouse.API.Application.Validations
{
    public class DashBoardSelectTopInwardCommandValidator : AbstractValidator<DashBoardSelectTopInwardCommand>
    {
        public DashBoardSelectTopInwardCommandValidator()
        {
            RuleFor(order => order.searchByMounth).GreaterThanOrEqualTo(0).WithMessage(ValidatorString.GetMessageNotNull("Tháng phải nằm trong khoảng 1 đến 12")).LessThanOrEqualTo(12).WithMessage(ValidatorString.GetMessageNotNull("Tháng phải nằm trong khoảng 1 đến 12"));
            RuleFor(order => order.searchByYear).GreaterThanOrEqualTo(0).WithMessage(ValidatorString.GetMessageNotNull("Năm phải nằm trong khoảng 1999 đến 2050")).LessThanOrEqualTo(2050).WithMessage(ValidatorString.GetMessageNotNull("Năm phải nằm trong khoảng 1999 đến 2050"));
        }
    }
}