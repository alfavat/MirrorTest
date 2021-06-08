using Business.Constants;
using Entity.Dtos;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Business.ValidationRules.FluentValidation
{
    public class NewsAgencyValidator : AbstractValidator<List<NewsAgencyAddDto>>
    {
        public NewsAgencyValidator()
        {
            RuleFor(p => p).NotNull().Must(f => f.Count > 0).WithMessage(Messages.EmptyParameter);
            RuleFor(p => p).Must(f => (int)f.First().NewsAgencyEntityId > 0).WithMessage(Messages.EmptyParameter);
        }
    }
}
