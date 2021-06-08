using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class OptionUpdateDtoValidator : AbstractValidator<OptionUpdateDto>
    {
        public OptionUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.WebsiteTitle).NotEmpty().WithMessage(ValidationMessages.EmptyWebsiteTitle);
            RuleFor(p => p.WebsiteTitle).MaximumLength(50).WithMessage(ValidationMessages.WebsiteTitleCharacterLimit);

            RuleFor(p => p.Address).MaximumLength(250).WithMessage(ValidationMessages.AddressCharacterLimit);

            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);
            RuleFor(p => p.Email).MaximumLength(50).WithMessage(ValidationMessages.EmailCharacterLimit);

            //RuleFor(p => p.Fax).MaximumLength(20).WithMessage(ValidationMessages.FaxMaxCharacterLimit);

            //RuleFor(p => p.SeoDescription).MaximumLength(250).WithMessage(ValidationMessages.SeoDescriptionMaxCharacterLimit);

            //RuleFor(p => p.SeoKeywords).MaximumLength(250).WithMessage(ValidationMessages.SeoKeywordsMaxCharacterLimit);

            //RuleFor(p => p.Telephone).MaximumLength(20).WithMessage(ValidationMessages.PhoneMaxCharacterLimit);

            RuleFor(p => p.WebsiteSlogan).MaximumLength(250).WithMessage(ValidationMessages.WebsiteSloganMaxCharacterLimit);

        }
    }

    public class OptionAddDtoValidator : AbstractValidator<OptionAddDto>
    {
        public OptionAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(Messages.EmptyParameter);

            RuleFor(p => p.WebsiteTitle).NotEmpty().WithMessage(ValidationMessages.EmptyWebsiteTitle);
            RuleFor(p => p.WebsiteTitle).MaximumLength(50).WithMessage(ValidationMessages.WebsiteTitleCharacterLimit);

            RuleFor(p => p.Address).MaximumLength(250).WithMessage(ValidationMessages.AddressCharacterLimit);

            RuleFor(p => p.Email).EmailAddress().WithMessage(ValidationMessages.EmailValidation);
            RuleFor(p => p.Email).MaximumLength(50).WithMessage(ValidationMessages.EmailCharacterLimit);

            //RuleFor(p => p.Fax).MaximumLength(20).WithMessage(ValidationMessages.FaxMaxCharacterLimit);

            //RuleFor(p => p.SeoDescription).MaximumLength(250).WithMessage(ValidationMessages.SeoDescriptionMaxCharacterLimit);

            //RuleFor(p => p.SeoKeywords).MaximumLength(250).WithMessage(ValidationMessages.SeoKeywordsMaxCharacterLimit);

            //RuleFor(p => p.Telephone).MaximumLength(20).WithMessage(ValidationMessages.PhoneMaxCharacterLimit);

            RuleFor(p => p.WebsiteSlogan).MaximumLength(250).WithMessage(ValidationMessages.WebsiteSloganMaxCharacterLimit);
        }
    }


}
