using FluentValidation;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Validations.BaseValidator
{
    public class BaseSearchModelValidator : AbstractValidator<BaseSearchModel>
    {
        public BaseSearchModelValidator()
        {
            RuleFor(order => order.PageIndex).GreaterThan(1).LessThan(1000)
                .WithMessage(ValidatorString.GetMessageToMin(1));
            RuleFor(order => order.PageIndex).LessThan(1000).WithMessage(ValidatorString.GetMessageToMin(1000));
            RuleFor(order => order.PageNumber).GreaterThan(10).WithMessage(ValidatorString.GetMessageToMax(1));
            RuleFor(order => order.PageNumber).LessThan(1000).WithMessage(ValidatorString.GetMessageToMin(1000));
        }
    }
}