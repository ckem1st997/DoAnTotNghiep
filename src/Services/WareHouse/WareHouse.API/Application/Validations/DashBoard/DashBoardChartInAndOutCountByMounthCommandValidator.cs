using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WareHouse.API.Application.Queries.DashBoard;

namespace WareHouse.API.Application.Validations
{
    public class DashBoardChartInAndOutCountByMounthCommandValidator : AbstractValidator<DashBoardChartInAndOutCountByMouthCommand>
    {
        public DashBoardChartInAndOutCountByMounthCommandValidator()
        {
            RuleFor(order => order.Year).GreaterThanOrEqualTo(1999).WithMessage("Năm phải lớn hơn 1999 !");
        }
    }
}