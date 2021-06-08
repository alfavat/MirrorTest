using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsHitAddDtoValidator : AbstractValidator<NewsHitAddDto>
    {
        public NewsHitAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
            RuleFor(p => p.NewsId).GreaterThan(0).WithMessage(ValidationMessages.EmptyNewsId);
            RuleFor(p => p.NewsHitComeFromEntityId).GreaterThan(0).WithMessage(ValidationMessages.EmptyNewsHitComeFromEntityId);
        }
    }


}
