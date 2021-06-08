using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsCommentUpdateDtoValidator : AbstractValidator<NewsCommentUpdateDto>
    {
        public NewsCommentUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.Title).NotEmpty().WithMessage(ValidationMessages.EmptyNewsCommentTitle);
            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.NewsCommentTitleCharacterLimit);

            RuleFor(p => p.NewsId).NotEmpty().WithMessage(ValidationMessages.EmptyNewsId);
            RuleFor(p => p.Content).MaximumLength(250).WithMessage(ValidationMessages.ContentMaxCharacterLimit);
        }
    }

    public class NewsCommentAddDtoValidator : AbstractValidator<NewsCommentAddDto>
    {
        public NewsCommentAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);


            RuleFor(p => p.Title).NotEmpty().WithMessage(ValidationMessages.EmptyNewsCommentTitle);
            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.NewsCommentTitleCharacterLimit);

            RuleFor(p => p.NewsId).NotEmpty().WithMessage(ValidationMessages.EmptyNewsId);
            RuleFor(p => p.Content).MaximumLength(250).WithMessage(ValidationMessages.ContentMaxCharacterLimit);
        }
    }


}
