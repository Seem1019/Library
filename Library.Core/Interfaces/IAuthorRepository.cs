using Library.Core.DTOs;

namespace Library.Core.Interfaces
{
    /// <summary>
    /// Provides read and write operations for Author entity.
    /// </summary>
    public interface IAuthorRepository : IRepository<AuthorDto>
    {
        // Add any additional methods specific to the Author entity here.
    }
}
