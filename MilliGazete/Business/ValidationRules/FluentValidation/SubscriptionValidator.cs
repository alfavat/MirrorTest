using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SubscriptionAddDtoValidator : AbstractValidator<SubscriptionAddDto>
    {
        public SubscriptionAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);

            RuleFor(p => p.CityId).NotEmpty().WithMessage(ValidationMessages.EmptyCityId);

            RuleFor(p => p.DistrictId).NotEmpty().WithMessage(ValidationMessages.EmptyDistrictId);

            RuleFor(p => p.FullName).NotEmpty().WithMessage(ValidationMessages.EmptyFullName);
            RuleFor(p => p.FullName).MaximumLength(100).WithMessage(ValidationMessages.FullNameCharacterLimit);

            RuleFor(p => p.Email).NotEmpty().WithMessage(ValidationMessages.EmptyEmail);
            RuleFor(p => p.Email).MaximumLength(200).WithMessage(ValidationMessages.EmailMaxCharacterLimit);
            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);

            RuleFor(p => p.Phone).NotEmpty().WithMessage(ValidationMessages.EmptyPhone);
            RuleFor(p => p.Phone).MaximumLength(20).WithMessage(ValidationMessages.PhoneMaxCharacterLimit);

            RuleFor(p => p.Address).NotEmpty().WithMessage(ValidationMessages.EmptyAddress);
            RuleFor(p => p.Address).MaximumLength(500).WithMessage(ValidationMessages.AddressMaxCharacterLimit);

            RuleFor(p => p.Description).NotEmpty().WithMessage(ValidationMessages.EmptyDescription);
        }
    }


}
