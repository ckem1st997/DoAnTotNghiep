using FluentValidation;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Validations.BaseValidator
{
    public class BaseSearchModelValidator: AbstractValidator<BaseSearchModel>
    {
        public BaseSearchModelValidator()
        {
            RuleFor(order => order.PageIndex).GreaterThanOrEqualTo(1).WithMessage(ValidatorString.GetMessageToMin(1));
            RuleFor(order => order.PageIndex).LessThanOrEqualTo(1000).WithMessage(ValidatorString.GetMessageToMin(1000));
            RuleFor(order => order.PageNumber).GreaterThanOrEqualTo(1).WithMessage(ValidatorString.GetMessageToMax(1));
            RuleFor(order => order.PageNumber).LessThanOrEqualTo(1000).WithMessage(ValidatorString.GetMessageToMin(1000));
        }
    }
}