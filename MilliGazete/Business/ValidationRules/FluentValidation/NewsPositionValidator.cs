using Business.Constants;
using Entity.Dtos;
using FluentValidation;
using System.Collections.Generic;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsPositionValidator : AbstractValidator<List<NewsPositionAddDto>>
    {
        public NewsPositionValidator()
        {
            RuleFor(p => p).NotNull().Must(f => f.Count > 0).WithMessage(Messages.EmptyParameter);
        }
    }
}
