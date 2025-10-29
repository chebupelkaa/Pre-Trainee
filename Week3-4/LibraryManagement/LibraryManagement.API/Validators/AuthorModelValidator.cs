using FluentValidation;
using LibraryManagement.API.Models;

namespace LibraryManagement.API.Validators
{
    public class AuthorModelValidator : AbstractValidator<AuthorModel>
    {
        public AuthorModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty.");

            RuleFor(x => x.DateOfBirth).NotEmpty()
                .LessThan(DateTime.Now.AddYears(-10)).WithMessage("Author must be at least 10 years old.");
        }
    }
}
