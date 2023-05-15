using Library.Core.DTOs;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read and write operations for Book entity.
    /// </summary>
    public interface IBookRepository : IRepository<BookDto>
    {
        // Add any additional methods specific to the Book entity here.
    }
}
