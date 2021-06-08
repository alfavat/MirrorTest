using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class TagAddValidator : AbstractValidator<TagAddDto>
    {
        public TagAddValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
            RuleFor(p => p.TagName).NotEmpty().WithMessage(ValidationMessages.EmptyTagName);
            RuleFor(p => p.TagName).MaximumLength(50).WithMessage(ValidationMessages.TagNameCharacterLimit);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);
        }
    }

    public class TagUpdateValidator : AbstractValidator<TagUpdateDto>
    {
        public TagUpdateValidator()
        {
            RuleFor(p => p).NotEmpty().WithMessage(ValidationMessages.EmptyParameter);
            RuleFor(p => p.Id).NotEmpty().WithMessage(ValidationMessages.EmptyId);
            RuleFor(p => p.Id).GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);
            RuleFor(p => p.TagName).NotEmpty().WithMessage(ValidationMessages.EmptyTagName);
            RuleFor(p => p.TagName).MaximumLength(50).WithMessage(ValidationMessages.TagNameCharacterLimit);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);
        }
    }
}
