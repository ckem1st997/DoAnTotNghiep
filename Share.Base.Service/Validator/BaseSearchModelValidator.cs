using FluentValidation;
using Share.Base.Core.Extensions;
using Share.Base.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Base.Service.Validator
{
    public class BaseSearchModelValidator : AbstractValidator<BaseSearchModel>
    {
        public BaseSearchModelValidator()
        {
            RuleFor(order => order.Skip).GreaterThanOrEqualTo(0).LessThan(1000)
                .WithMessage(ValidatorString.GetMessageToMin(1));
            RuleFor(order => order.Take).GreaterThan(0).LessThan(1000).WithMessage(ValidatorString.GetMessageToMin(1000));
        }
    }
}
