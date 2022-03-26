using FluentValidation;
using WareHouse.API.Application.Extensions;
using WareHouse.API.Application.Queries.BaseModel;

namespace WareHouse.API.Application.Validations.BaseValidator
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