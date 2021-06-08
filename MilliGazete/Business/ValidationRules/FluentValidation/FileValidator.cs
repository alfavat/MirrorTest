using Business.Constants;
using Entity.Models;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FileValidator : AbstractValidator<File>
    {
        public FileValidator()
        {
            RuleFor(p => p.FileName).Length(4, 250).WithMessage(ValidationMessages.FileNameCharacterLimit);
            RuleFor(p => p.FileSizeKb).NotEmpty().WithMessage(ValidationMessages.EmptyFileSize);
        }
    }
}
