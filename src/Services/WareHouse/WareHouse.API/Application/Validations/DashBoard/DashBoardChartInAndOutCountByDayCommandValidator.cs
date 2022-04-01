using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Queries.DashBoard;

namespace WareHouse.API.Application.Validations
{
    public class DashBoardChartInAndOutCountByDayCommandValidator : AbstractValidator<DashBoardChartInAndOutCountByDayCommand>
    {
        public DashBoardChartInAndOutCountByDayCommandValidator()
        {
            RuleFor(order => order.fromDate).NotNull().WithMessage("Chưa chọn ngày bắt đầu !");
            RuleFor(order => order.toDate).NotNull().WithMessage("Chưa chọn ngày kết thúc !");
         

        }
    }
}