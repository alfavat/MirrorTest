using Business.Constants;
using Entity.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{

    public class UserOperationClaimUpdateValidator : AbstractValidator<UserOperationClaimUpdateDto>
    {
        public UserOperationClaimUpdateValidator()
        {
            RuleFor(p => p.UserId).NotEmpty().GreaterThan(-1).WithMessage(ValidationMessages.EmptyUserId);
            RuleFor(p => p.OperationClaimIds).NotEmpty().WithMessage(ValidationMessages.EmptyClaimId);
        }
    }

}
