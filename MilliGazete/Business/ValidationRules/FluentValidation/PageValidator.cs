using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class PageUpdateDtoValidator : AbstractValidator<PageUpdateDto>
    {
        public PageUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.TitleCharacterLimit);

            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);


        }
    }

    public class PageAddDtoValidator : AbstractValidator<PageAddDto>
    {
        public PageAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;

            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.TitleCharacterLimit);

            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);
        }
    }


}
