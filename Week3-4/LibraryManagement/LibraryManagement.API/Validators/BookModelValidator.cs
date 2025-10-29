using FluentValidation;
using LibraryManagement.API.Models;

namespace LibraryManagement.API.Validators
{
    public class BookModelValidator : AbstractValidator<BookModel>
    {
        public BookModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");

            RuleFor(x => x.PublishedYear)
                .InclusiveBetween(1700, DateTime.Now.Year)
                .WithMessage($"Published year must be between 1700 and {DateTime.Now.Year}");

            RuleFor(x => x.AuthorId)
                .GreaterThan(0).WithMessage("Author ID must be greater than 0");
        }
    }
}
