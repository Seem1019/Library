using Library.Core.DTOs;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read and write operations for the AuthorHasBook entity.
    /// </summary>
    public interface IAuthorHasBookRepository : IRepository<AuthorHasBookDto>
    {
        // Add any additional methods specific to the AuthorHasBook entity here.
    }
}
