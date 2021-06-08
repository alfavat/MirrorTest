using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CurrencyAddDtoValidator : AbstractValidator<CurrencyAddDto>
    {
        public CurrencyAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
        }
    }
}
