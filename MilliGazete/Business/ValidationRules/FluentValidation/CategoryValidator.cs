using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(p => p.Id).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);

            RuleFor(p => p.CategoryName).NotEmpty().WithMessage(ValidationMessages.EmptyCategoryName);
            RuleFor(p => p.CategoryName).MaximumLength(50).WithMessage(ValidationMessages.CategoryNameCharacterLimit);

            RuleFor(p => p.Url).NotEmpty().WithMessage(ValidationMessages.EmptyUrl);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);

            RuleFor(p => p.Symbol).NotEmpty().WithMessage(ValidationMessages.EmptySymbol);
            RuleFor(p => p.Symbol).MaximumLength(50).WithMessage(ValidationMessages.SymbolMaxCharacterLimit);

            RuleFor(p => p.StyleCode).NotEmpty().WithMessage(ValidationMessages.EmptyStyleCode);
            RuleFor(p => p.StyleCode).MaximumLength(250).WithMessage(ValidationMessages.StyleCodeMaxCharacterLimit);

            RuleFor(p => p).Must(p => !p.ParentCategoryId.HasValue || p.Id != p.ParentCategoryId.Value).WithMessage(ValidationMessages.CategoryAndParentCategoryMustBeDifferent);
        }
    }

    public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
    {
        public CategoryAddDtoValidator()
        {
            RuleFor(p => p).NotNull().WithMessage(ValidationMessages.EmptyParameter); ;

            RuleFor(p => p.CategoryName).NotEmpty().WithMessage(ValidationMessages.EmptyCategoryName);
            RuleFor(p => p.CategoryName).MaximumLength(50).WithMessage(ValidationMessages.CategoryNameCharacterLimit);

            RuleFor(p => p.Url).NotEmpty().WithMessage(ValidationMessages.EmptyUrl);
            RuleFor(p => p.Url).MaximumLength(250).WithMessage(ValidationMessages.UrlMaxCharacterLimit);

            RuleFor(p => p.Symbol).NotEmpty().WithMessage(ValidationMessages.EmptySymbol);
            RuleFor(p => p.Symbol).MaximumLength(50).WithMessage(ValidationMessages.SymbolMaxCharacterLimit);

            RuleFor(p => p.StyleCode).NotEmpty().WithMessage(ValidationMessages.EmptyStyleCode);
            RuleFor(p => p.StyleCode).MaximumLength(250).WithMessage(ValidationMessages.StyleCodeMaxCharacterLimit);
        }
    }


}
