using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage(ValidationMessages.EmptyUserName);
            RuleFor(p => p.UserName).Length(6, 50).WithMessage(ValidationMessages.UserNameCharacterLimit);
            RuleFor(p => p.Email).Length(5, 50).WithMessage(ValidationMessages.EmailCharacterLimit);
            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);
            RuleFor(p => p.Password).Length(6, 50).WithMessage(ValidationMessages.PasswordCharacterLimit);
        }
    }
}
