using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class QuestionAnswerUpdateDtoValidator : AbstractValidator<QuestionAnswerUpdateDto>
    {
        public QuestionAnswerUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.Answer).NotEmpty().WithMessage(ValidationMessages.EmptyAnswer);

        }
    }

    public class QuestionAnswerAddDtoValidator : AbstractValidator<QuestionAnswerAddDto>
    {
        public QuestionAnswerAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;

            RuleFor(p => p.Answer).MaximumLength(250).WithMessage(ValidationMessages.EmptyAnswer);

        }
    }


}
