using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{

    public class UserCategoryRelationUpdateDtoValidator : AbstractValidator<UserCategoryRelationUpdateDto>
    {
        public UserCategoryRelationUpdateDtoValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyId);
        }
    }
}
