using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsBookmarkAddValidator : AbstractValidator<NewsBookmarkAddDto>
    {
        public NewsBookmarkAddValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
            RuleFor(p => p.NewsId).NotEmpty().WithMessage(ValidationMessages.EmptyNewsId);
        }
    }
}
