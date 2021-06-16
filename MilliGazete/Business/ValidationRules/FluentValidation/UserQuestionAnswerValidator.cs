using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserQuestionAnswerAddDtoValidator : AbstractValidator<UserQuestionAnswerAddDto>
    {
        public UserQuestionAnswerAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);
        }
    }


}
