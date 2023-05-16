using Contacts.Core.CustomEntities;
using Library.Core.Entities;
using Library.Core.QueryFilters;
using System.Threading.Tasks;

namespace Library.Core.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Fetches a single book by its ID.
        /// </summary>
        Task<Book> GetBook(int id);

        /// <summary>
        /// Fetches a list of books applying given filters.
        /// </summary>
        PagedList<Book> GetBooks(BookQueryFilter filters);

        /// <summary>
        /// Adds a new book to the repository.
        /// </summary>
        Task InsertBook(Book book);

        /// <summary>
        /// Updates an existing book's details.
        /// </summary>
        Task<bool> UpdateBook(Book book);

        /// <summary>
        /// Deletes a book from the repository.
        /// </summary>
        Task<bool> DeleteBook(int id);

    }
}
