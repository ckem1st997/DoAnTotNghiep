using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WareHouse.API.Application.Commands.Models;

namespace WareHouse.API.Application.Validations
{
    public partial class UnitCommandValidator : AbstractValidator<UnitCommands>
    {
        public UnitCommandValidator()
        {
            RuleFor(order => order.UnitName).NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
            RuleFor(order => order.UnitName).NotNull().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));
        }

        private readonly IMediator _dbContext;

        public UnitCommandValidator(IMediator dbContext)
        {
            _dbContext = dbContext;

            RuleFor(order => order.UnitName)
                .NotEmpty().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"))
                .NotNull().WithMessage(ValidatorString.GetMessageNotNull("Tên sản phẩm"));

            RuleFor(order => order.Id)
                .MustAsync(UnitIdExists).WithMessage("Id đầu vào không tồn tại trong cơ sở dữ liệu.");
        }

        private async Task<bool> UnitIdExists(string Id, CancellationToken cancellationToken)
        {
            var existingUnit = await _dbContext.Send(new UnitCommands()
            {
                Id = Id
            });

            return existingUnit != null;
        }



    }
}