using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class QuestionUpdateDtoValidator : AbstractValidator<QuestionUpdateDto>
    {
        public QuestionUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.QuestionText).NotEmpty().WithMessage(ValidationMessages.EmptyQuestionText);
            RuleFor(p => p.QuestionText).MaximumLength(250).WithMessage(ValidationMessages.QuestionTextMaxCharacterLimit);

        }
    }

    public class QuestionAddDtoValidator : AbstractValidator<QuestionAddDto>
    {
        public QuestionAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;
            RuleFor(p => p.QuestionText).NotEmpty().WithMessage(ValidationMessages.EmptyQuestionText);
            RuleFor(p => p.QuestionText).MaximumLength(250).WithMessage(ValidationMessages.QuestionTextMaxCharacterLimit);

        }
    }


}
