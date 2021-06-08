using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage(ValidationMessages.EmptyNewPassword);
            RuleFor(p => p.NewPassword).Length(6, 50).WithMessage(ValidationMessages.NewPasswordCharacterLimit);
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage(ValidationMessages.EmptyConfirmPassword);
            RuleFor(p => p.ConfirmPassword).Length(6, 50).WithMessage(ValidationMessages.ConfirmPasswordCharacterLimit);
            RuleFor(p => p.NewPassword).Equal(f => f.ConfirmPassword).WithMessage(ValidationMessages.PasswordConflict);
        }
    }
}
