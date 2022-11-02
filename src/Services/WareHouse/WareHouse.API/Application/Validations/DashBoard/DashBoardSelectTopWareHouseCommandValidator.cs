using System;
using FluentValidation;
using WareHouse.API.Application.Commands.Models;
using WareHouse.API.Application.Queries.DashBoard;
using WareHouse.API.Application.Queries.Report;

namespace WareHouse.API.Application.Validations
{
    public class DashBoardSelectTopWareHouseCommandValidator : AbstractValidator<DashBoardSelectTopWareHouseBeginningCommand>
    {
        public DashBoardSelectTopWareHouseCommandValidator()
        {
            RuleFor(order => order.order).Custom((order, context) =>
   {
       if (string.IsNullOrEmpty(order))
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