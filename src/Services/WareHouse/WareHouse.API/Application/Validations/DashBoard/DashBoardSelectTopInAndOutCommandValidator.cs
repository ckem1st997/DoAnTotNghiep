using System;
using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Queries.DashBoard;
using WareHouse.API.Application.Queries.Report;

namespace WareHouse.API.Application.Validations
{
    public class DashBoardSelectTopInAndOutCommandValidator : AbstractValidator<BaseDashboardCommands>
    {
        public DashBoardSelectTopInAndOutCommandValidator()
        {
            RuleFor(order => order.searchByMounth).GreaterThanOrEqualTo(0).WithMessage("Tháng phải nằm trong khoảng 1 đến 12").LessThanOrEqualTo(12).WithMessage("Tháng phải nằm trong khoảng 1 đến 12");
            RuleFor(order => order.searchByYear).GreaterThanOrEqualTo(0).WithMessage("Năm phải nằm trong khoảng 1999 đến 2050").LessThanOrEqualTo(2050).WithMessage("Năm phải nằm trong khoảng 1999 đến 2050");
            RuleFor(order => order.selectTopWareHouseBook).NotNull().WithMessage("Chưa chọn kiểu lọc");
            RuleFor(order => order.searchByMounth).GreaterThanOrEqualTo(0).WithMessage("Tháng phải nằm trong khoảng 1 đến 12").LessThanOrEqualTo(12).WithMessage("Tháng phải nằm trong khoảng 1 đến 12");
            RuleFor(order => order.order).Custom((order, context) =>
            {
                if(string.IsNullOrEmpty(order))
                {
                    context.AddFailure("Chưa chọn kiểu sắp xếp");
                    return;
                }
                if (!order.Equals(SoftStatic.ASC) && !order.Equals(SoftStatic.DESC))
                {
                    context.AddFailure("Chọn kiểu sắp xếp sai");
                }
            });

        }
    }
}