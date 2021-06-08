using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsCommentLikeUpdateDtoValidator : AbstractValidator<NewsCommentLikeUpdateDto>
    {
        public NewsCommentLikeUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);
            RuleFor(p => p.NewsCommentId).NotEmpty().WithMessage(ValidationMessages.EmptyNewsCommentId);
        }
    }

    public class NewsCommentLikeAddDtoValidator : AbstractValidator<NewsCommentLikeAddDto>
    {
        public NewsCommentLikeAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
            RuleFor(p => p.NewsCommentId).NotEmpty().WithMessage(ValidationMessages.EmptyNewsCommentId);
        }
    }


}
