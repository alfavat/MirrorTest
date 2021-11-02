using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewspaperAddDtoValidator : AbstractValidator<NewspaperAddDto>
    {
        public NewspaperAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);

            RuleFor(p => p.Name).MaximumLength(50).WithMessage(ValidationMessages.TitleCharacterLimit);
        }
    }


}
