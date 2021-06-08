using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ArticleUpdateDtoValidator : AbstractValidator<ArticleUpdateDto>
    {
        public ArticleUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.AuthorId).NotEmpty().WithMessage(ValidationMessages.EmptyAuthorId);
            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.TitleCharacterLimit);

            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);

            
        }
    }

    public class ArticleAddDtoValidator : AbstractValidator<ArticleAddDto>
    {
        public ArticleAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;

            RuleFor(p => p.AuthorId).NotEmpty().WithMessage(ValidationMessages.EmptyAuthorId);
            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.TitleCharacterLimit);

            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);
        }
    }


}
