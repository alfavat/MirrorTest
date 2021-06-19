using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ReporterUpdateDtoValidator : AbstractValidator<ReporterUpdateDto>
    {
        public ReporterUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.FullName).NotEmpty().WithMessage(ValidationMessages.EmptyFullName);

        }
    }

    public class ReporterAddDtoValidator : AbstractValidator<ReporterAddDto>
    {
        public ReporterAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;

            RuleFor(p => p.FullName).NotEmpty().WithMessage(ValidationMessages.EmptyFullName);

        }
    }


}
