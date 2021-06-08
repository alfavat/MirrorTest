using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class AuthorUpdateDtoValidator : AbstractValidator<AuthorUpdateDto>
    {
        public AuthorUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.NameSurename).MaximumLength(150).WithMessage(ValidationMessages.AuthorNameCharacterLimit);

            RuleFor(p => p.Url).NotEmpty().WithMessage(ValidationMessages.EmptyUrl);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);

        }
    }

    public class AuthorAddDtoValidator : AbstractValidator<AuthorAddDto>
    {
        public AuthorAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);

            RuleFor(p => p.NameSurename).MaximumLength(150).WithMessage(ValidationMessages.AuthorNameCharacterLimit);

            RuleFor(p => p.Url).NotEmpty().WithMessage(ValidationMessages.EmptyUrl);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);
        }
    }


}
