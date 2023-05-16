using FluentValidation;
using Library.Core.DTOs;

namespace Library.Infrastructure.Validators
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(book => book.Isbn)
                .NotEmpty().WithMessage("ISBN is required.");

            RuleFor(book => book.PublisherId)
                .NotEmpty().WithMessage("PublisherId is required.");
            RuleFor(book => book.Authors)
                .NotEmpty().WithMessage("Authors is required.");

            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(0, 50).WithMessage("Title cannot be more than 50 characters.");

            RuleFor(book => book.Synopsis)
                .NotEmpty().WithMessage("Synopsis is required.");

            RuleFor(book => book.NumberOfPages)
                .NotEmpty().WithMessage("NumberOfPages is required.")
                .Must(pages => int.TryParse(pages, out int n) && n > 0).WithMessage("NumberOfPages must be a positive integer.");
        }
    }
}
