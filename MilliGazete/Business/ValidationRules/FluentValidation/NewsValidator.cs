using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsAddDtoValidator : AbstractValidator<NewsAddDto>
    {
        public NewsAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;

            RuleFor(p => p.NewsAgencyEntityId).GreaterThan(-1).WithMessage(ValidationMessages.EmptyNewsAgencyEntityId);
            RuleFor(p => p.NewsTypeEntityId).GreaterThan(-1).WithMessage(ValidationMessages.EmptyNewsTypeEntityId);

            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.NewsTitleCharacterLimit);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);
            RuleFor(p => p.SeoTitle).MaximumLength(250).WithMessage(ValidationMessages.SeoTitleMaxCharacterLimit);
            RuleFor(p => p.SocialTitle).MaximumLength(250).WithMessage(ValidationMessages.SocialTitleMaxCharacterLimit);
        }
    }


}
