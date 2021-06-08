using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<RegisterDto>
    {
        public UserValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage(ValidationMessages.EmptyUserName);
            RuleFor(p => p.UserName).Length(6, 50).WithMessage(ValidationMessages.UserNameCharacterLimit);
            RuleFor(p => p.Email).Length(5, 50).WithMessage(ValidationMessages.EmailCharacterLimit);
            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);
            RuleFor(p => p.Password).Length(6,50).WithMessage(ValidationMessages.PasswordCharacterLimit);
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage(ValidationMessages.PasswordConflict);
        }
    }

    public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);
            RuleFor(p => p.UserName).NotEmpty().WithMessage(ValidationMessages.EmptyUserName);
            RuleFor(p => p.UserName).Length(5, 50).WithMessage(ValidationMessages.UserNameCharacterLimit);
            RuleFor(p => p.Email).Length(5, 50).WithMessage(ValidationMessages.EmailCharacterLimit);
            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage(ValidationMessages.FirstNameCharacterLimit);
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage(ValidationMessages.LastNameCharacterLimit);
        }
    }

    //
    public class CurrentUserUpdateValidator : AbstractValidator<CurrentUserUpdateDto>
    {
        public CurrentUserUpdateValidator()
        {
            RuleFor(p => p.UserName).NotEmpty().WithMessage(ValidationMessages.EmptyUserName);
            RuleFor(p => p.UserName).Length(5, 50).WithMessage(ValidationMessages.UserNameCharacterLimit);
            RuleFor(p => p.Email).Length(5, 50).WithMessage(ValidationMessages.EmailCharacterLimit);
            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage(ValidationMessages.FirstNameCharacterLimit);
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage(ValidationMessages.LastNameCharacterLimit);
        }
    }

}
