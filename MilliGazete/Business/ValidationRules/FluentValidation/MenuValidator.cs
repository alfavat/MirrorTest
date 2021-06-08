using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class MenuUpdateDtoValidator : AbstractValidator<MenuUpdateDto>
    {
        public MenuUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.Title).NotEmpty().WithMessage(ValidationMessages.EmptyTitle);
            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.TitleCharacterLimit);

            RuleFor(p => p.Url).NotEmpty().WithMessage(ValidationMessages.EmptyUrl);

            RuleFor(p => p).Must(p => !p.ParentMenuId.HasValue || p.Id != p.ParentMenuId.Value).WithMessage(ValidationMessages.MenuAndParentMenuMustBeDifferent);
        }
    }

    public class MenuAddDtoValidator : AbstractValidator<MenuAddDto>
    {
        public MenuAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter);

            RuleFor(p => p.Title).NotEmpty().WithMessage(ValidationMessages.EmptyTitle);
            RuleFor(p => p.Title).MaximumLength(250).WithMessage(ValidationMessages.TitleCharacterLimit);

            RuleFor(p => p.Url).NotEmpty().WithMessage(ValidationMessages.EmptyUrl);
        }
    }


}
