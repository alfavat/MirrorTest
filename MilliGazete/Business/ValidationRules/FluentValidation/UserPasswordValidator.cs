using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserPasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public UserPasswordValidator()
        {
            RuleFor(p => p.Password).NotEmpty().WithMessage(ValidationMessages.EmptyPassword).OverridePropertyName("");
            RuleFor(p => p.Password).Length(6, 50).WithMessage(ValidationMessages.PasswordCharacterLimit).OverridePropertyName("");
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage(ValidationMessages.EmptyNewPassword).OverridePropertyName("");
            RuleFor(p => p.NewPassword).Length(6, 50).WithMessage(ValidationMessages.NewPasswordCharacterLimit).OverridePropertyName("");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage(ValidationMessages.EmptyConfirmPassword).OverridePropertyName("");
            RuleFor(p => p.ConfirmPassword).Length(6, 50).WithMessage(ValidationMessages.ConfirmPasswordCharacterLimit).OverridePropertyName("");
            RuleFor(p => p.NewPassword).Equal(f => f.ConfirmPassword).WithMessage(ValidationMessages.PasswordConflict).OverridePropertyName("");
            RuleFor(p => p.NewPassword).NotEqual(f => f.Password).WithMessage(ValidationMessages.SamePassword).OverridePropertyName("");
        }
    }
}
