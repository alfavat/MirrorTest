using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AgencyNewsCopyDtoValidator : AbstractValidator<AgencyNewsCopyDto>
    {
        public AgencyNewsCopyDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
            RuleFor(p => p.AgencyNewsId).NotNull().WithMessage(ValidationMessages.EmptyId);
        }
    }


}
